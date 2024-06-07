using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;

namespace TravelAgency.Infrastructure.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly DatabaseContext _context;

        public CountryRepository(DatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public List<Country> GetAll()
        {
            return _context.Country.ToList();
        }

        public Country Get(int id)
        {
            return _context.Country.FirstOrDefault(c => c.CountryId == id);
        }
        public bool CountryExists(int id)
        {
            var result = _context.Country.Any(x => x.CountryId == id);
            return result;
        }
    }
}
