namespace Test_Announcement.API.Models
{
    public class AddAnnouncementRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }
    }
}
