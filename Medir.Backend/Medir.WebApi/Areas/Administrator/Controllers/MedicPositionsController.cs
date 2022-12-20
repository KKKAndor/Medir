using AutoMapper;
using Medir.Application.MedicPositions.Commands.CreateMedicPosition;
using Medir.Application.MedicPositions.Commands.DeleteMedicPosition;
using Medir.Application.MedicPositions.Commands.UpdateMedicPosition;
using Medir.Application.MedicPositions.Queries.GetMedicPositionDetails;
using Medir.Application.MedicPositions.Queries.GetMedicPositionList;
using Medir.WebApi.Areas.Administrator.Models.MedicPositions;
using Medir.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Medir.WebApi.Areas.Administrator.Controllers
{
    [Produces("application/json")]
    [Route("api/[Area]/[controller]")]
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class MedicPositionsController : BaseController
    {
        private readonly IMapper _mapper;

        public MedicPositionsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of MedicPositions
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /MedicPositions
        /// </remarks>
        /// <returns>Returns MedicPositionsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("GetMedicPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicPositionsListVm>> GetAllMedicPositions(Guid MedicId)
        {            
            var query = new GetMedicPositionListQuery
            {
                MedicId = MedicId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the MedicPosition by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /MedicPositions/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="GetMedicPositionDetailsQuery">MedicPosition id (Guid)</param>
        /// <returns>Returns MedicPositionDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("GetMedicPositionId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicPositionDetailsVm>> GetMedicPosition([FromQuery] GetMedicPositionDetailsQuery GetMedicPositionDetailsQuery)
        {
            //var query = new GetMedicPositionDetailsQuery
            //{
            //    MedicId = GetMedicPositionDetailsQuery.MedicId,
            //    PolyclinicId = GetMedicPositionDetailsQuery.PolyclinicId
            //};
            var vm = await Mediator.Send(GetMedicPositionDetailsQuery);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the MedicPosition
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /MedicPositions
        /// {
        ///     "MedicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "PolyclinicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "DateOnPosition": "10-01-1999"
        /// }
        /// </remarks>
        /// <param name="createMedicPositionDto">createMedicPositionDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateMedicPosition([FromBody] CreateMedicPositionDto createMedicPositionDto)
        {
            var command = _mapper.Map<CreateMedicPositionCommand>(createMedicPositionDto);
            var Id = await Mediator.Send(command);
            return Ok(Id);
        }

        /// <summary>
        /// Updates the MedicPosition
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /MedicPositions
        /// {
        ///     "MedicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "PolyclinicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "DateOnPosition": "10-01-1999"
        /// }
        /// </remarks> 
        /// <param name="updateMedicPositionDto">UpdateMedicPositionDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateMedicPosition([FromBody] UpdateMedicPositionDto updateMedicPositionDto)
        {
            var command = _mapper.Map<UpdateMedicPositionCommand>(updateMedicPositionDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes  the MedicPosition
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// Delete /MedicPositions/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="DeleteMedicPositionCommand">MedicPosition id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpDelete("DeleteMedicPositionId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteMedicPosition([FromQuery] DeleteMedicPositionCommand DeleteMedicPositionCommand)
        {
            //var query = new DeleteMedicPositionCommand
            //{
            //    MedicId = DeleteMedicPositionCommand.MedicId,
            //    PolyclinicId = DeleteMedicPositionCommand.PolyclinicId
            //};
            await Mediator.Send(DeleteMedicPositionCommand);
            return NoContent();
        }
    }
}
