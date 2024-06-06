using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Exceptions;
using TravelAgency.Application.Services;
using TravelAgency.SharedKernel.Dto;
using TravelAgency.SharedKernel.Dto.Hotel;

namespace TravelAgency.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
        {
            this._hotelService = hotelService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDto>> Get()
        {
            var result = _hotelService.GetAll();
            _logger.LogDebug("Pulled a list of all hotels");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelDto> Get(int id)
        {
            var result = _hotelService.GetById(id);
            _logger.LogDebug($"Pulled a hotel with id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateHotelDto dto)
        {
            var id = _hotelService.Create(dto);
            _logger.LogDebug($"Created new hotel with id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateHotelDto dto)
        {
            if (id != dto.HotelId)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _hotelService.Update(dto);
            _logger.LogDebug($"Updated hotel with id = {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _hotelService.Delete(id);
            _logger.LogDebug($"Deleted hotel with id = {id}");
            return NoContent();
        }
    }

}
