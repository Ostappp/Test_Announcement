using Test_Announcement.DataAccess.Models;

namespace Test_Announcement.API.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<(Announcement, float)>> GetSimilarAnnouncements(int id, int count);
    }
}
