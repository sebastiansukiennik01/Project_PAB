using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Contracts
{
    public interface ICountryRepository : IRepository<Country>
    {
        public List<Country> GetAll();
        public Country Get(int id);
        public bool CountryExists(int id);
    }
}
