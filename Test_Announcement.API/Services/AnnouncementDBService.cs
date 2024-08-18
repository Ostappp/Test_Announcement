using Microsoft.EntityFrameworkCore;
using Test_Announcement.API.Interfaces;
using Test_Announcement.API.Models;
using Test_Announcement.DataAccess.Context;
using Test_Announcement.DataAccess.Models;

namespace Test_Announcement.API.Services
{
    public class AnnouncementDBService(AnnouncementDbContext dbContext) : IAnnouncementDBService
    {
        public async Task<Announcement> CreateAnnouncement(AddAnnouncementRequest request)
        {
            Announcement newAnnouncement = new Announcement
            {
                Title = request.Title,
                Description = request.Description,
                EventDate = request.EventDate,
                DateOfPublishing = DateTime.UtcNow,
            };

            await dbContext.Announcements.AddAsync(newAnnouncement);
            await dbContext.SaveChangesAsync();

            return newAnnouncement;
        }

        public async Task DeleteAnnouncement(int id)
        {
            var announcement = await dbContext.Announcements.FindAsync(id)
                ?? throw new KeyNotFoundException($"There is no {nameof(Announcement)} with id: {id}");
            dbContext.Announcements.Remove(announcement);

            await dbContext.SaveChangesAsync();
        }

        public async Task<Announcement> GetAnnouncement(int id)
        {
            var announcement = await dbContext.Announcements.FindAsync(id)
                ?? throw new KeyNotFoundException($"There is no {nameof(Announcement)} with id: {id}");

            return announcement;
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncements()
        {
            var announcement = await dbContext.Announcements.ToListAsync();

            return announcement;
        }

        public async Task<Announcement> UpdateAnnouncement(UpdateAnnouncementRequest updateRequest)
        {
            var announcement = await dbContext.Announcements.FindAsync(updateRequest.Id) 
                ?? throw new KeyNotFoundException($"There is no {nameof(Announcement)} with id: {updateRequest.Id}");
            
            if (!string.IsNullOrEmpty(updateRequest.Title))
                announcement.Title = updateRequest.Title;

            if (!string.IsNullOrEmpty(updateRequest.Description))
                announcement.Description = updateRequest.Description;

            if (updateRequest.EventDate != null)
                announcement.EventDate = updateRequest.EventDate.Value;

            dbContext.Update(announcement);
            await dbContext.SaveChangesAsync();

            return announcement;
        }
    }
}
