using AutoMapper;
using MediatR;
using Medir.Application.Polyclinics.Commands.CreatePolyclinic;
using Medir.Application.Polyclinics.Commands.DeletePolyclinic;
using Medir.Application.Polyclinics.Commands.UpdatePolyclinic;
using Medir.Application.Polyclinics.Queries.GetPolyclinicDetails;
using Medir.Application.Polyclinics.Queries.GetPolyclinicList;
using Medir.WebApi.Areas.Administrator.Models.Polyclinics;
using Medir.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medir.WebApi.Areas.Administrator.Controllers
{
    [Produces("application/json")]
    [Route("api/[Area]/[controller]")]
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class PolyclinicsController : BaseController
    {
        private readonly IMapper _mapper;

        public PolyclinicsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of Polyclinics
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Polyclinics
        /// </remarks>
        /// <returns>Returns PolyclinicsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PolyclinicsListVm>> GetAllPolyclinics()
        {
            var query = new GetPolyclinicListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the Polyclinic by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Polyclinics/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="PolyclinicId">Polyclinic id (Guid)</param>
        /// <returns>Returns PolyclinicDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("{PolyclinicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PolyclinicDetailsVm>> GetPolyclinic(Guid PolyclinicId)
        {
            var query = new GetPolyclinicDetailsQuery
            {
                PolyclinicId = PolyclinicId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the Polyclinic
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /Polyclinics
        /// {       
        ///     "PolyclinicName": "Gregory Hous Polyclinic",
        ///     "PolyclinicPhoto": "PhotoUrl"
        /// }
        /// </remarks>
        /// <param name="createPolyclinicDto">createPolyclinicDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreatePolyclinic([FromBody] CreatePolyclinicDto createPolyclinicDto)
        {
            var command = _mapper.Map<CreatePolyclinicCommand>(createPolyclinicDto);
            var Id = await Mediator.Send(command);
            return Ok(Id);
        }

        /// <summary>
        /// Updates the Polyclinic
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /Polyclinics
        /// {
        ///     "PolyclinicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE"
        ///     "PolyclinicName": "Gregory Hous Polyclinic",
        ///     "PolyclinicPhoto": "PhotoUrl"
        /// }
        /// </remarks> 
        /// <param name="updatePolyclinicDto">UpdatePolyclinicDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePolyclinic([FromBody] UpdatePolyclinicDto updatePolyclinicDto)
        {
            var command = _mapper.Map<UpdatePolyclinicCommand>(updatePolyclinicDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes  the Polyclinic
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// Delete /Polyclinics/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="PolyclinicId">Polyclinic id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpDelete("{PolyclinicId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePolyclinic(Guid PolyclinicId)
        {
            var command = new DeletePolyclinicCommand
            {
                PolyclinicId = PolyclinicId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
