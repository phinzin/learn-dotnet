using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkingAPI.Models.Domain;
using WalkingAPI.Models.DTO;
using WalkingAPI.Repositories;

namespace WalkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController(IRegionRepository regionRepository, IMapper mapper) : ControllerBase
    {
        // GET: api/Region
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetRegions()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<RegionDto>>(regionsDomain));
        }

        // GET: api/Region/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetRegion(Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // PUT: api/Region/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutRegion(Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = mapper.Map<Region>(updateRegionRequestDto);
            await regionRepository.UpdateAsync(id, regionDomain);

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST: api/Region
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddRegionRequestDto>> PostRegion(AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomain = mapper.Map<Region>(addRegionRequestDto);

            await regionRepository.AddAsync(regionDomain);
            // Map Domain Model back to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return CreatedAtAction("GetRegion", new { id = regionDomain.Id }, regionDto);
        }

        // DELETE: api/Region/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
    }
}