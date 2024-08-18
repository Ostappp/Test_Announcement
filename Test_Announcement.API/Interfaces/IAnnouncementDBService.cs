using Test_Announcement.API.Models;
using Test_Announcement.DataAccess.Models;

namespace Test_Announcement.API.Interfaces
{
    public interface IAnnouncementDBService
    {
        Task<Announcement> CreateAnnouncement(AddAnnouncementRequest request);

        Task<Announcement> GetAnnouncement(int id);

        Task<IEnumerable<Announcement>> GetAnnouncements();

        Task<Announcement> UpdateAnnouncement(UpdateAnnouncementRequest updateRequest);

        Task DeleteAnnouncement(int id);
    }
}
