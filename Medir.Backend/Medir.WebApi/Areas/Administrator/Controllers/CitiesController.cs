using AutoMapper;
using MediatR;
using Medir.Application.Cities.Commands.CreateCity;
using Medir.Application.Cities.Commands.DeleteCity;
using Medir.Application.Cities.Commands.UpdateCity;
using Medir.Application.Cities.Queries.GetCityDetails;
using Medir.Application.Cities.Queries.GetCityList;
using Medir.WebApi.Areas.Administrator.Models.Cities;
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
    public class CitiesController : BaseController
    {
        private readonly IMapper _mapper;

        public CitiesController(IMapper mapper) => _mapper = mapper;

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
        public async Task<ActionResult<CitiesListVm>> GetAllCities()
        {
            var query = new GetCityListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the City by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Cities/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="CityId">City id (Guid)</param>
        /// <returns>Returns CityDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("{CityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CityDetailsVm>> GetCity(Guid CityId)
        {
            var query = new GetCityDetailsQuery
            {
                CityId = CityId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the City
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// POST /Cities
        /// {       
        ///     "CityName": "Gregory Hous city"
        /// }
        /// </remarks>
        /// <param name="createCityDto">createCityDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateCity([FromBody] CreateCityDto createCityDto)
        {
            var command = _mapper.Map<CreateCityCommand>(createCityDto);
            var Id = await Mediator.Send(command);
            return Ok(Id);
        }

        /// <summary>
        /// Updates the City
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /Cities
        /// {
        ///     "CityId": "45B336E1-D01D-4C4F-9A83-58E60E65C2BE"
        ///     "CityName": "Gregory Hous city"
        /// }
        /// </remarks> 
        /// <param name="updateCityDto">UpdateCityDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateCityDto updateCityDto)
        {
            var command = _mapper.Map<UpdateCityCommand>(updateCityDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes  the City
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// Delete /Cities/45B336E1-D01D-4C4F-9A83-58E60E65C2BE
        /// </remarks>
        /// <param name="CityId">City id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        [HttpDelete("{CityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCity(Guid CityId)
        {
            var command = new DeleteCityCommand
            {
                CityId = CityId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
