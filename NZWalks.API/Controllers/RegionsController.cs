using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDBContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map domain model with DTO
            /*var regionDto = new List<RegionDTO>();
            foreach (var region in regionsDomain)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl

                });
            }*/ 
            var regionDto = mapper.Map<List<RegionDTO>>(regionsDomain);

            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //map regionDTO with Reg Domain
            /*var regionDto = new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };*/
            var regionDto=mapper.Map<RegionDTO>(regionDomain);
            return Ok(regionDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or convert DTO to Domain model

            /*var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };*/

            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create Region

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map domain model to DTO 
            /*var regionDto = new RegionDTO {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };*/
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            /*var regionDomainModel = new Region
            {
                Code=updateRegionRequestDto.Code,
                Name= updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };*/
            var regionDomainModel=mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);


            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain to Dto
            /*var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };*/
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regDomainModel = await regionRepository.DeletAsync(id);

            if(regDomainModel == null)
            {
                return NotFound();
            }

            /*dbContext.Regions.Remove(regDomainModel);
            await dbContext.SaveChangesAsync();*/
            return Ok(regDomainModel);
        }

    }
}
