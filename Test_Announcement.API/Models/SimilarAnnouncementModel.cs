namespace Test_Announcement.API.Models
{
    public class SimilarAnnouncementModel
    {
        public BriefAnnouncementModel briefAnnouncement { get; set; }
        public float SimilarityIndex { get; set; }
    }
}
