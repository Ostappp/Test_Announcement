namespace Test_Announcement.API.Models
{
    public class AnnouncementResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateOfPublishing { get; set; }

        public DateTime EventDate { get; set; }
    }
}
