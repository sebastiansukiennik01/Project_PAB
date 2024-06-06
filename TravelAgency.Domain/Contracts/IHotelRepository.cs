using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Contracts
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        public List<Hotel> GetAll();
        public Hotel Get(int id);
        public bool CityExists(int id);
    }
}
