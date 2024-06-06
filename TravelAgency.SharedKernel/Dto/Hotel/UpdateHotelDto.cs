namespace TravelAgency.SharedKernel.Dto.Hotel
{
    public class UpdateHotelDto
    {
        public int HotelId { get; set; }
        public string? Name { get; set; }
        public int Rate { get; set; }
        public int CityId { get; set; }
    }
}
