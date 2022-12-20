using AutoMapper;
using Medir.Application.Appointments.Commands.CreateAppointment;
using Medir.Application.Appointments.Queries.GetAppointmentList;
using Medir.Application.Cities.Queries.GetCityList;
using Medir.Application.Common.Pagination;
using Medir.Application.User.Queries.GetMedicAvailabiltyForUserList;
using Medir.Application.User.Queries.GetMedicsForUserList;
using Medir.Application.User.Queries.GetPositionsForUserList;
using Medir.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Medir.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of Cities
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Cities
        /// </remarks>
        /// <returns>Returns CitiesListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CitiesListVm>> GetAllCitiesForChoice()
        {
            var query = new GetCityListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of PositionsForUser
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /User
        /// </remarks>
        /// <param name="CityId">City Id (Guid)</param>
        /// <returns>Returns PolyclinicsByCityListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("PositionsForUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PositionsForUserListVm>> GetPositionsForUserList([FromQuery] Guid CityId)
        {
            var query = new GetPositionsForUserListQuery
            {
                CityId  = CityId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of MedicsForUser
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /User
        /// </remarks>
        /// <param name="GetMedicsForUserListQuery">GetMedicsByPositionAndPoliclinicListQuery (GetMedicsByPositionAndPoliclinicListQuery)</param>
        /// <param name="Parameters"></param>
        /// <returns>Returns MedicsForUserListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("MedicsForUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult <MedicsForUserListVm>> GetMedicsForUserList(
            GetMedicsForUserListQuery GetMedicsForUserListQuery, 
            [FromQuery] MedicsForUserParameters Parameters)
        {
            GetMedicsForUserListQuery.Parameters = Parameters;

            var vm = await Mediator.Send(GetMedicsForUserListQuery);
               
            var metadata = new
            {
                vm.MedicsForUser.TotalCount,
                vm.MedicsForUser.PageSize,
                vm.MedicsForUser.CurrentPage,
                vm.MedicsForUser.TotalPages,
                vm.MedicsForUser.HasNext,
                vm.MedicsForUser.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of MedicsForUser
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /User
        /// </remarks>
        /// <param name="GetMedicsForUserListQuery">GetMedicsByPositionAndPoliclinicListQuery (GetMedicsByPositionAndPoliclinicListQuery)</param>
        /// <returns>Returns MedicsForUserListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("AllMedicsForUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicsForUserListVm>> GetAllMedicsForUserList(
            [FromQuery] GetMedicsForUserListQuery GetMedicsForUserListQuery)
        {
            var vm = await Mediator.Send(GetMedicsForUserListQuery);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of MedicAvailabilityForUserListVm
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /User
        /// </remarks>
        /// <returns>Returns MedicAvailabiltyOnDayVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("GetMedicAvailabilitiesForUserList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MedicAvailabilityForUserListVm>> GetMedicAvailabilitiesForUserList(
            [FromQuery] GetMedicAvailabilityForUserListQuery GetMedicAvailabilityForUserListQuery)
        {
            var vm = await Mediator.Send(GetMedicAvailabilityForUserListQuery);
            return Ok(vm);
        }


        /// <summary>
        /// Creates the Appointment
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /User
        /// </remarks>
        /// <param name="CreateAppointment">CreateAppointmentDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost("CreateAppointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAppointment([FromBody] CreateAppointmentDto CreateAppointment)
        {
            var command = _mapper.Map<CreateAppointmentCommand>(CreateAppointment);
            var Id = await Mediator.Send(command);
            return Ok(Id);
        }

        /// <summary>
        /// Gets the list of Appointments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /User
        /// </remarks>
        /// <param name="AppointmentListForUser">GetAppointmentList (GetAppointmentListQuery)</param>
        /// <returns>Returns AppointmentsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("AppointmentListForUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<AppointmentsListVm>> GetAppointments(
            [FromQuery] GetAppointmentListQuery AppointmentListForUser)
        {
            var vm = await Mediator.Send(AppointmentListForUser);
            return Ok(vm);
        }        
    }
}
