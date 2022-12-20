using AutoMapper;
using Medir.Application.Common.Pagination;
using Medir.Application.Medics.Commands.CreateMedic;
using Medir.Application.Medics.Commands.DeleteMedic;
using Medir.Application.Medics.Commands.UpdateMedic;
using Medir.Application.Medics.Queries.GetMedicDetails;
using Medir.Application.Medics.Queries.GetMedicList;
using Medir.WebApi.Areas.Administrator.Models.Medics;
using Medir.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Medir.WebApi.Areas.Administrator.Controllers
{
    [Produces("application/json")]
    [Route("api/[Area]/[controller]")]
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class MedicsController : BaseController
    {
        private readonly IMapper _mapper;

        public MedicsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of Medics
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Medics
        /// </remarks>
        /// <returns>Returns MedicsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicsListVm>> GetAllMedics([FromQuery] MedicsParameters parameters)
        {
            var query = new GetMedicListQuery 
            {
                Parameters = parameters
            };
            var vm = await Mediator.Send(query);
            var metadata = new
            {
                vm.Medics.TotalCount,
                vm.Medics.PageSize,
                vm.Medics.CurrentPage,
                vm.Medics.TotalPages,
                vm.Medics.HasNext,
                vm.Medics.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(vm);
        }

        /// <summary>
        /// Gets the Medic by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Medics/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="MedicId">Medic id (Guid)</param>
        /// <returns>Returns MedicDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("{MedicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicDetailsVm>> GetMedic(Guid MedicId)
        {
            var query = new GetMedicDetailsQuery
            {
                MedicId = MedicId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the Medic
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /Medics
        /// {       
        ///     "MedicFullName": "Gregory Hous",
        ///     "ShortDescription": "The best doctor",
        ///     "FullDescription": "Yeap, the best",
        ///     "MedicPhoneNumber": "88002000122",,
        ///     "MedicPhoto": "PhotoUrl"
        /// }
        /// </remarks>
        /// <param name="createMedicDto">createMedicDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateMedic([FromBody] CreateMedicDto createMedicDto)
        {
            var command = _mapper.Map<CreateMedicCommand>(createMedicDto);
            var Id = await Mediator.Send(command);
            return Ok(Id);
        }

        /// <summary>
        /// Updates the Medic
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /Medics
        /// {
        ///     "MedicId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE"
        ///     "MedicFullName": "Gregory Hous",
        ///     "ShortDescription": "The best doctor",
        ///     "FullDescription": "I said he is the best, didn't I?",
        ///     "MedicPhoneNumber": "88002000122",
        ///     "MedicPhoto": "PhotoUrl"
        /// }
        /// </remarks> 
        /// <param name="updateMedicDto">UpdateMedicDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateMedic([FromBody] UpdateMedicDto updateMedicDto)
        {
            var command = _mapper.Map<UpdateMedicCommand>(updateMedicDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes  the Medic
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// Delete /Medics/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="MedicId">Medic id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpDelete("{MedicId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteMedic(Guid MedicId)
        {
            var command = new DeleteMedicCommand
            {
                MedicId = MedicId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
