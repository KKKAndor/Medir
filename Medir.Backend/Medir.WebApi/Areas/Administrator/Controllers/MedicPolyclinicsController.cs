using AutoMapper;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList;
using Medir.Application.MedicPolyclinics.Commands.CreateMedicPolyclinic;
using Medir.Application.MedicPolyclinics.Commands.DeleteMedicPolyclinic;
using Medir.Application.MedicPolyclinics.Commands.UpdateMedicPolyclinic;
using Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicDetails;
using Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList;
using Medir.Domain;
using Medir.WebApi.Areas.Administrator.Models.MedicPolyclinics;
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
    public class MedicPolyclinicsController : BaseController
    {
        private readonly IMapper _mapper;

        public MedicPolyclinicsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of MedicPolyclinics
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /MedicPolyclinics
        /// </remarks>
        /// <returns>Returns MedicPolyclinicsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("GetMedicPolyclinic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicPolyclinicsListVm>> GetAllMedicPolyclinics(Guid MedicId, 
            [FromQuery] MedicPolyclinicsParameters parameters)
        {
            var query = new GetMedicPolyclinicListQuery
            {
                MedicId = MedicId,
                Parameters = parameters
            };

            var vm = await Mediator.Send(query);

            var metadata = new
            {
                vm.MedicPolyclinics.TotalCount,
                vm.MedicPolyclinics.PageSize,
                vm.MedicPolyclinics.CurrentPage,
                vm.MedicPolyclinics.TotalPages,
                vm.MedicPolyclinics.HasNext,
                vm.MedicPolyclinics.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                        
            return Ok(vm);
        }

        /// <summary>
        /// Gets the MedicPolyclinic by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /MedicPolyclinics/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="GetMedicPolyclinicDetailsQuery">MedicPolyclinic id (Guid)</param>
        /// <returns>Returns MedicPolyclinicDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("GetMedicPolyclinicId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicPolyclinicDetailsVm>> GetMedicPolyclinic([FromQuery] GetMedicPolyclinicDetailsQuery GetMedicPolyclinicDetailsQuery)
        {
            var vm = await Mediator.Send(GetMedicPolyclinicDetailsQuery);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the MedicPolyclinic
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /MedicPolyclinics
        /// {
        ///     "MedicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "PolyclinicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "Price": "200"
        /// }
        /// </remarks>
        /// <param name="createMedicPolyclinicDto">createMedicPolyclinicDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateMedicPolyclinic([FromBody] CreateMedicPolyclinicDto createMedicPolyclinicDto)
        {
            var command = _mapper.Map<CreateMedicPolyclinicCommand>(createMedicPolyclinicDto);
            var Id = await Mediator.Send(command);
            return Ok(Id);
        }

        /// <summary>
        /// Updates the MedicPolyclinic
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /MedicPolyclinics
        /// {
        ///     "MedicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "PolyclinicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE",
        ///     "Price": "200"
        /// }
        /// </remarks> 
        /// <param name="updateMedicPolyclinicDto">UpdateMedicPolyclinicDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateMedicPolyclinic([FromBody] UpdateMedicPolyclinicDto updateMedicPolyclinicDto)
        {
            var command = _mapper.Map<UpdateMedicPolyclinicCommand>(updateMedicPolyclinicDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes  the MedicPolyclinic
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// Delete /MedicPolyclinics/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="DeleteMedicPolyclinicCommand">MedicPolyclinic id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpDelete("DeleteMedicPolyclinicId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteMedicPolyclinic([FromQuery] DeleteMedicPolyclinicCommand DeleteMedicPolyclinicCommand)
        {
            //var query = new DeleteMedicPolyclinicCommand
            //{
            //    MedicId = DeleteMedicPolyclinicCommand.MedicId,
            //    PolyclinicId = DeleteMedicPolyclinicCommand.PolyclinicId
            //};
            await Mediator.Send(DeleteMedicPolyclinicCommand);
            return NoContent();
        }
    }
}
