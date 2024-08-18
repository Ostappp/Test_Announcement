using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Test_Announcement.API.Interfaces;
using Test_Announcement.DataAccess.Context;
using Test_Announcement.DataAccess.Models;

namespace Test_Announcement.API.Services
{
    public class AnnouncementService(AnnouncementDbContext dbContext) : IAnnouncementService
    {
        public async Task<IEnumerable<(Announcement, float)>> GetSimilarAnnouncements(int id, int count)
        {
            var announcement = await dbContext.Announcements.FindAsync(id)
                ?? throw new KeyNotFoundException($"There is no {nameof(Announcement)} with id: {id}");

            var announcements = await dbContext.Announcements.ToListAsync();

            IEnumerable<string> keyWords = GetPlainText($"{announcement.Title} {announcement.Description}")
                .Split(' ').Distinct();

            var similarityIndexes = announcements.Where(a=>a.Id != id).Select(a => new { id = a.Id, txt = $"{a.Title} {a.Description}" })
                .Select(a => new { Id = a.id, Index = GetSimilarityScore(keyWords, GetPlainText(a.txt)) })
                .OrderByDescending(i => i.Index).Take(count);

            var result = similarityIndexes
                .Join(announcements, index => index.Id, announcement => announcement.Id, (index, announcement) => (announcement, index.Index))
                .ToList();
            
            return result;
        }
        private string GetPlainText(string text) => Regex.Replace(text, @"[\p{P}]", string.Empty);

        private float GetSimilarityScore(IEnumerable<string> keyWords, string plainText)
        {
            IEnumerable<string> textKeyWords = plainText.Split(' ').Distinct();
            IEnumerable<string> commonWords = textKeyWords.Intersect(keyWords);
            return commonWords.Count() / (float)keyWords.Count();
        }
    }
}
