using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkingAPI.CustomActionFilters;
using WalkingAPI.Models.Domain;
using WalkingAPI.Models.DTO;
using WalkingAPI.Repositories;

namespace WalkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController(IWalkRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Walk?>>> GetWalks()
        {
            return await repository.GetAllAsync();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Walk>> GetWalk([FromRoute] Guid id)
        {
            var walkDomainModel = await repository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutWalk([FromRoute] Guid id,
            [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            var existingWalk = await repository.UpdateAsync(id, walkDomainModel);

            if (existingWalk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(existingWalk));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Walk>> PostWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomain = mapper.Map<Walk>(addWalkRequestDto);
            await repository.AddAsync(walkDomain);
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walkDomain = await repository.DeleteAsync(id);
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }
    }
}