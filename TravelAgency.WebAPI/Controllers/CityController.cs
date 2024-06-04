using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Infrastructure;
using TravelAgency.SharedKernel.Dto;

namespace TravelAgency.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : Controller
    {
        private readonly DatabaseContext _context;

        public CityController(DatabaseContext _context)
        {
            this._context = _context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> Get()
        {
            
            var cities = _context.City.ToList();

            List<CityDto> result = new List<CityDto>();

            foreach (var city in cities)
            {
                result.Add(new CityDto()
                {
                    CityId = city.CityId,
                    Name = city.Name,
                });
            }

            return Ok(result);
        }
    }
}
