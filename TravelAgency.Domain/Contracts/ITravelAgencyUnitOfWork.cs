namespace TravelAgency.Domain.Contracts
{
    public interface ITravelAgencyUnitOfWork : IDisposable
    {
        ICountryRepository CountryRepository { get; }
        ICityRepository CityRepository { get; }
        IHotelRepository HotelRepository { get; }
        IOfferRepository OfferRepository { get; }


        void Commit();
    }
}
