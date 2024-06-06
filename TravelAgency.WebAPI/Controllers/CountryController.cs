using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Exceptions;
using TravelAgency.Application.Services;
using TravelAgency.SharedKernel.Dto.Country;

namespace TravelAgency.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CityController> _logger;

        public CountryController(ICountryService countryService, ILogger<CityController> logger)
        {
            this._countryService = countryService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CountryDto>> Get()
        {
            var result = _countryService.GetAll();
            _logger.LogDebug("Pulled a list of all countries");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CountryDto> Get(int id)
        {
            var result = _countryService.GetById(id);
            _logger.LogDebug($"Pulled a country with id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateCountryDto dto)
        {
            var id = _countryService.Create(dto);
            _logger.LogDebug($"Created new country with id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateCountryDto dto)
        {
            if (id != dto.CountryId)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _countryService.Update(dto);
            _logger.LogDebug($"Updated country with id = {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _countryService.Delete(id);
            _logger.LogDebug($"Deleted country with id = {id}");
            return NoContent();
        }
    }

}
