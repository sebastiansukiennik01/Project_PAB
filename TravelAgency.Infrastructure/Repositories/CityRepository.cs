using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;

namespace TravelAgency.Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly DatabaseContext _context;

        public CityRepository(DatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public List<City> GetAll()
        {
            return _context.City.Include(c => c.Country).ToList();
        }

        public City Get(int id)
        {
            return _context.City.Include(c => c.Country).FirstOrDefault(c => c.CityId == id);
        }

        public bool CountryExists(int id)
        {
            var result = _context.Country.Any(x => x.CountryId ==  id);
            return result;
        }
    }
}
