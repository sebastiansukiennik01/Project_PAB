using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Exceptions;
using TravelAgency.Application.Services;
using TravelAgency.SharedKernel.Dto;

namespace TravelAgency.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityService cityService, ILogger<CityController> logger)
        {
            this._cityService = cityService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CityDto>> Get()
        {
            var result = _cityService.GetAll();
            _logger.LogDebug("Pulled a list of all cities");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CityDto> Get(int id)
        {
            var result = _cityService.GetById(id);
            _logger.LogDebug($"Pulled a city with id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateCityDto dto)
        {
            var id = _cityService.Create(dto);
            _logger.LogDebug($"Created new city with id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateCityDto dto)
        {
            if (id != dto.CityId)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _cityService.Update(dto);
            _logger.LogDebug($"Updated city with id = {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _cityService.Delete(id);
            _logger.LogDebug($"Deleted city with id = {id}");
            return NoContent();
        }
    }

}
