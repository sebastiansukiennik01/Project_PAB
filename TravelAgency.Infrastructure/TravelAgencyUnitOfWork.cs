using TravelAgency.Domain.Contracts;

namespace TravelAgency.Infrastructure
{
    // UoW implementation
    public class TravelAgencyUnitOfWork : ITravelAgencyUnitOfWork
    {
        private readonly DatabaseContext _context;

        public IOfferRepository OfferRepository { get; }
        public ICityRepository CityRepository { get; }

        public TravelAgencyUnitOfWork(DatabaseContext context, IOfferRepository offerRepository, ICityRepository cityRepository)
        {
            this._context = context;
            this.OfferRepository = offerRepository;
            this.CityRepository = cityRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
