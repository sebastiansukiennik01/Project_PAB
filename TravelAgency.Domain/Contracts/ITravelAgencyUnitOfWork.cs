namespace TravelAgency.Domain.Contracts
{
    public interface ITravelAgencyUnitOfWork : IDisposable
    {
        IOfferRepository OfferRepository { get; }
        ICityRepository CityRepository { get; }

        void Commit();
    }
}
