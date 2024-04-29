using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;

        public RegionsController(NZWalksDBContext dbContext)
        {
            this.dbContext=dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();

            //Map domain model with DTO
            var regionDto=new List<RegionDTO>();
            foreach (var region in regionsDomain)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code= region.Code,
                    Name = region.Name,
                    RegionImageUrl= region.RegionImageUrl

                });
            }

            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{{id:Guid}}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.Find(id);

            if(regionDomain == null)
            {
                return NotFound();
            }
            //map regionDTO with Reg Domain
            var regionDto = new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}
