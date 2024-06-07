using TravelAgency.SharedKernel.Dto.City;
using TravelAgency.SharedKernel.Dto.Hotel;

namespace TravelAgency.SharedKernel.Dto.Offer
{
    public class UpdateOfferDto
    {
        public int OfferId { get; set; }
        public int HotelId { get; set; }
        public decimal Price { get; set; }
        public int DepartureCityId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
