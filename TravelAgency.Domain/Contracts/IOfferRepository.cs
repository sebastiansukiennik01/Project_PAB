

using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Contracts
{
    public interface IOfferRepository : IRepository<Offer>
    {
        public List<Offer> GetAll();
        public Offer Get(int id);
        public bool OfferExists(int id);
    }
}
