using TravelAgency.SharedKernel.Dto.City;

namespace TravelAgency.SharedKernel.Dto.Hotel
{
    public class CreateHotelDto
    {
        public string? Name { get; set; }
        public int Rate { get; set; }
        public int CityId { get; set; }
    }
}
