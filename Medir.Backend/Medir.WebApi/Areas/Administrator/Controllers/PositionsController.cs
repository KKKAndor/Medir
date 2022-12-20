using AutoMapper;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.Positions.Commands.CreatePosition;
using Medir.Application.Positions.Commands.DeletePosition;
using Medir.Application.Positions.Commands.UpdatePosition;
using Medir.Application.Positions.Queries.GetPositionDetails;
using Medir.Application.Positions.Queries.GetPositionList;
using Medir.WebApi.Areas.Administrator.Models.Positions;
using Medir.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Medir.WebApi.Areas.Administrator.Controllers
{
    [Produces("application/json")]
    [Route("api/[Area]/[controller]")]
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class PositionsController : BaseController
    {
        private readonly IMapper _mapper;

        public PositionsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of Positions
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Positions
        /// </remarks>
        /// <returns>Returns PositionsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PositionsListVm>> GetAllPositions([FromQuery] PositionsParameters parameters)
        {
            var query = new GetPositionListQuery 
            {
                Parameters = parameters
            };
            var vm = await Mediator.Send(query);
            var metadata = new
            {
                vm.Positions.TotalCount,
                vm.Positions.PageSize,
                vm.Positions.CurrentPage,
                vm.Positions.TotalPages,
                vm.Positions.HasNext,
                vm.Positions.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(vm);
        }

        /// <summary>
        /// Gets the Position by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Positions/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="PositionId">Position id (Guid)</param>
        /// <returns>Returns PositionDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("{PositionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PositionDetailsVm>> GetPosition(Guid PositionId)
        {
            var query = new GetPositionDetailsQuery
            {
                PositionId = PositionId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the Position
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /Positions
        /// {       
        ///     "PositionName": "Gregory Hous position"
        /// }
        /// </remarks>
        /// <param name="createPositionDto">createPositionDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreatePosition([FromBody] CreatePositionDto createPositionDto)
        {
            var command = _mapper.Map<CreatePositionCommand>(createPositionDto);
            var Id = await Mediator.Send(command);
            return Ok(Id);
        }

        /// <summary>
        /// Updates the Position
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /Positions
        /// {
        ///     "PositionId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE"
        ///     "PositionName": "Gregory Hous position is busy"
        /// }
        /// </remarks> 
        /// <param name="updatePositionDto">UpdatePositionDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePosition([FromBody] UpdatePositionDto updatePositionDto)
        {
            var command = _mapper.Map<UpdatePositionCommand>(updatePositionDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes  the Position
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// Delete /Positions/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="PositionId">Position id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpDelete("{PositionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePosition(Guid PositionId)
        {
            var command = new DeletePositionCommand
            {
                PositionId = PositionId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
