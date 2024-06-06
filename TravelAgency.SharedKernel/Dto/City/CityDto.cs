using TravelAgency.SharedKernel.Dto.Country;

namespace TravelAgency.SharedKernel.Dto.City
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string? Name { get; set; }
        public CountryDto? Country { get; set; }

    }
}
