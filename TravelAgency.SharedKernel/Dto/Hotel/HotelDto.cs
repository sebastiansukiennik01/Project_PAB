using TravelAgency.SharedKernel.Dto.City;

namespace TravelAgency.SharedKernel.Dto.Hotel
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string? Name { get; set; }
        public int Rate { get; set; }
        public CityDto? City { get; set; }
    }
}
