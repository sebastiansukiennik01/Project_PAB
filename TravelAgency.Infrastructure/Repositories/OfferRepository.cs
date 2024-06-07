using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;

namespace TravelAgency.Infrastructure.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private readonly DatabaseContext _context;

        public OfferRepository(DatabaseContext context)
            : base(context)
        {
            _context = context;
        }
        public List<Offer> GetAll()
        {
            return _context.Offer
                .Include(offer => offer.DepartureCity)
                .ThenInclude(city => city.Country)
                .Include(offer => offer.Hotel)
                .ThenInclude(hotel => hotel.City)
                .ThenInclude(city => city.Country)
                .ToList();
        }

        public Offer Get(int id)
        {
            return _context.Offer
                .Include(offer => offer.DepartureCity)
                .ThenInclude(city => city.Country)
                .Include(offer => offer.Hotel)
                .ThenInclude(hotel => hotel.City)
                .ThenInclude(city => city.Country)
                .FirstOrDefault(offer => offer.OfferId == id);
        }

        public bool OfferExists(int id)
        {
            var result = _context.Offer.Any(o => o.OfferId == id);
            return result;
        }
    }
}
