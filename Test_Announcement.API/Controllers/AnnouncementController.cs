using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Test_Announcement.API.Interfaces;
using Test_Announcement.API.Models;
using Test_Announcement.DataAccess.Models;

namespace Test_Announcement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController(IMapper mapper, IAnnouncementDBService dBService, IAnnouncementService announcementService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAnnouncementDBService _dbService = dBService;
        private readonly IAnnouncementService _announcementService = announcementService;

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAnnouncement(int id)
        {
            var announcement = _mapper.Map<AnnouncementResponse>(await _dbService.GetAnnouncement(id));

            return Ok(announcement);
        }

        [HttpGet]
        [Route("similars/{id}")]
        public async Task<IActionResult> GetSimilarAnnouncements(int id, int count = 3)
        {
            var result = await _announcementService.GetSimilarAnnouncements(id, count);
            List<SimilarAnnouncementModel> similarAnnouncements = result
                .Select(a => new SimilarAnnouncementModel
                {
                    briefAnnouncement = _mapper.Map<BriefAnnouncementModel>(a.Item1),
                    SimilarityIndex = a.Item2
                }).ToList();


            return Ok(similarAnnouncements);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            var result = await _dbService.GetAnnouncements();
            var announcements = result.Select(_mapper.Map<BriefAnnouncementModel>).ToList();

            return Ok(announcements);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateAnnouncement(AddAnnouncementRequest announcementRequest)
        {
            var announcement = _mapper.Map<AnnouncementResponse>(await _dbService.CreateAnnouncement(announcementRequest));

            return CreatedAtRoute(new { announcementId = announcement.Id }, announcement);
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateAnnouncement(UpdateAnnouncementRequest announcementRequest)
        {
            var updatedAnnouncement = _mapper.Map<AnnouncementResponse>(await _dbService.UpdateAnnouncement(announcementRequest));

            return Ok(updatedAnnouncement);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await _dbService.DeleteAnnouncement(id);

            return NoContent();
        }
    }
}
