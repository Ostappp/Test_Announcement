using AutoMapper;
using Test_Announcement.API.Models;
using Test_Announcement.DataAccess.Models;

namespace Test_Announcement.API.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<Announcement, AnnouncementResponse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Announcement, BriefAnnouncementModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
