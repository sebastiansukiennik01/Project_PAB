using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;

namespace TravelAgency.Infrastructure.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly DatabaseContext _context;

        public HotelRepository(DatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public List<Hotel> GetAll()
        {
            return _context.Hotel.Include(h => h.City).ThenInclude(c => c.Country).ToList();
        }

        public Hotel Get(int id)
        {
            return _context.Hotel.Include(h => h.City).ThenInclude(c => c.Country).FirstOrDefault(c => c.HotelId == id);
        }

        public bool HotelExists(int id)
        {
            var result = _context.Hotel.Any(c => c.HotelId ==  id);
            return result;
        }
    }
}
