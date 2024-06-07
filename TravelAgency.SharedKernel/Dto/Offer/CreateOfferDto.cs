namespace TravelAgency.SharedKernel.Dto.Offer
{
    public class CreateOfferDto
    {
        public int HotelId { get; set; }
        public decimal Price { get; set; }
        public int DepartureCityId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
