using AutoMapper;
using Medir.Application.MedicAvailabilities.Command.CreateMedicAvailability;
using Medir.Application.MedicAvailabilities.Command.DeleteMedicAvailability;
using Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList;
using Medir.WebApi.Areas.Administrator.Models.MedicAvailabilities;
using Medir.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medir.WebApi.Areas.Administrator.Controllers
{
    [Produces("application/json")]
    [Route("api/[Area]/[controller]")]
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class MedicAvailabilityController : BaseController
    {
        private readonly IMapper _mapper;

        public MedicAvailabilityController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of MedicAvailabiltyOnDay
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /MedicAvailabilty
        /// </remarks>
        /// <returns>Returns MedicAvailabiltyOnDayVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("GetMedicAvailabilitiesList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicAvailabilityListVm>> GetMedicAvailabilitiesList(
            [FromQuery] GetMedicAvailabilityListQuery GetMedicAvailabilityListQuery)
        {
            var vm = await Mediator.Send(GetMedicAvailabilityListQuery);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the MedicAvailability
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /MedicAvailability
        /// </remarks>
        /// <param name="CreateMedicAvailabilityDto">CreateMedicAvailabilityDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost("CreateMedicAvailability")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateMedicAvailability([FromBody] CreateMedicAvailabilityDto CreateMedicAvailabilityDto)
        {
            var command = _mapper.Map<CreateMedicAvailabilityCommand>(CreateMedicAvailabilityDto);
            var Id = await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete the MedicAvailability
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// DELETE /MedicAvailability
        /// </remarks>
        /// <param name="DeleteMedicAvailabilityCommand">DeleteMedicAvailabilityCommand object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpDelete("DeleteMedicAvailability")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteMedicAvailability([FromBody] DeleteMedicAvailabilityCommand DeleteMedicAvailabilityCommand)
        {
            var command = _mapper.Map<DeleteMedicAvailabilityCommand>(DeleteMedicAvailabilityCommand);
            var Id = await Mediator.Send(command);
            return NoContent();
        }
    }
}
