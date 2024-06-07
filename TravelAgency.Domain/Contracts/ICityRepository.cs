using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Contracts
{
    public interface ICityRepository : IRepository<City>
    {
        public List<City> GetAll();
        public City Get(int id);
        public bool CityExists(int id);
    }
}
