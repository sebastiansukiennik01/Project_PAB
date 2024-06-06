using TravelAgency.Domain.Contracts;

namespace TravelAgency.Infrastructure
{
    // UoW implementation
    public class TravelAgencyUnitOfWork : ITravelAgencyUnitOfWork
    {
        private readonly DatabaseContext _context;

        public ICountryRepository CountryRepository { get; }
        public IHotelRepository HotelRepository { get; }
        public ICityRepository CityRepository { get; }
        public IOfferRepository OfferRepository { get; }


        public TravelAgencyUnitOfWork(
            DatabaseContext context,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IHotelRepository  hotelRepository,
            IOfferRepository offerRepository
        )
        {
            this._context = context;
            this.OfferRepository = offerRepository;
            this.CityRepository = cityRepository;
            this.CountryRepository = countryRepository;
            this.HotelRepository = hotelRepository;
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
