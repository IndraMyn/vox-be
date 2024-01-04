using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vox.Data.OrganizersData;
using Vox.Data.Pagination;
using Vox.Data.SportEventData;
using Vox.Data.UserData;
using Vox.Services.OrganizersService;
using Vox.Services.SportEventsService;
using Vox.Utils;

namespace Vox.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/sport-events")]
    public class SportEventsController : ControllerBase
    {
        private readonly ILogger<SportEventsController> _logger;
        private readonly ISportEventService _sportEventService;
        public SportEventsController(ILogger<SportEventsController> logger, ISportEventService sportEventService)
        {
            _logger = logger;
            _sportEventService = sportEventService;
        }

        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        [Route("")]
        [ProducesResponseType(typeof(BasePaginationResponse<OrganizersResponse>), 200)]
        public async Task<IActionResult> List([FromQuery] SportEventsRequest request)
        {
            try
            {
                return Ok(await _sportEventService.findAll(request));
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
        public async Task<IActionResult> Create([FromBody] SportEventUpdateRequest request)
        {
            try
            {
                return Ok(await _sportEventService.create(request));
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
                return Ok(await _sportEventService.findById(id));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SportEventUpdateRequest request)
        {
            try
            {
                return Ok(await _sportEventService.update(id, request));
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
                await _sportEventService.delete(id);
                return Ok();
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }
    }
}
