using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vox.Data.OrganizersData;
using Vox.Data.Pagination;
using Vox.Data.UserData;
using Vox.Services.OrganizersService;
using Vox.Utils;

namespace Vox.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/organizers")]
    public class OrganizersController : ControllerBase
    {
        private readonly ILogger<OrganizersController> _logger;
        private readonly IOrganizersService _organizersService;
        public OrganizersController(ILogger<OrganizersController> logger, IOrganizersService organizersService)
        {
            _logger = logger;
            _organizersService = organizersService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("")]
        [ProducesResponseType(typeof(BasePaginationResponse<OrganizersResponse>), 200)]
        public async Task<IActionResult> List([FromQuery] BasePaginationRequest request)
        {
            try
            {
                return Ok(await _organizersService.findAll(request));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("")]
        [ProducesResponseType(typeof(UserDetailResponse), 200)]
        public async Task<IActionResult> Create([FromBody] OrganizerUpdateRequest request)
        {
            try
            {
                return Ok(await _organizersService.create(request));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{id}")]
        [ProducesResponseType(typeof(UserDetailResponse), 200)]
        public async Task<IActionResult> Detail(long id)
        {
            try
            {
                return Ok(await _organizersService.findById(id));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] OrganizerUpdateRequest request)
        {
            try
            {
                return Ok(await _organizersService.update(id, request));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [HttpDelete]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _organizersService.delete(id);
                return Ok();
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }
    }
}
