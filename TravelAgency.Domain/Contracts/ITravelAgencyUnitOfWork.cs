namespace TravelAgency.Domain.Contracts
{
    public interface ITravelAgencyUnitOfWork : IDisposable
    {
        IOfferRepository OfferRepository { get; }

        void Commit();
    }
}
