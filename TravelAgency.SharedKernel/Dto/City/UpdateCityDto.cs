namespace TravelAgency.SharedKernel.Dto.City
{
    public class UpdateCityDto
    {
        public int CityId { get; set; }
        public string? Name { get; set; }
        public int CountryId { get; set; }
    }
}
