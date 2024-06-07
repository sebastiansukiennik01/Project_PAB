using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Exceptions;
using TravelAgency.SharedKernel.Dto.Offer;
using TravelAgency.Application.Services.Interfaces;

namespace TravelAgency.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly ILogger<OfferController> _logger;

        public OfferController(IOfferService offerService, ILogger<OfferController> logger)
        {
            this._offerService = offerService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OfferDto>> Get()
        {
            var result = _offerService.GetAll();
            _logger.LogDebug("Pulled a list of all offers");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OfferDto> Get(int id)
        {
            var result = _offerService.GetById(id);
            _logger.LogDebug($"Pulled an offer with id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateOfferDto dto)
        {
            var id = _offerService.Create(dto);
            _logger.LogDebug($"Created new offer with id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateOfferDto dto)
        {
            if (id != dto.OfferId)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _offerService.Update(dto);
            _logger.LogDebug($"Updated offer with id = {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _offerService.Delete(id);
            _logger.LogDebug($"Deleted offer with id = {id}");
            return NoContent();
        }
    }

}
