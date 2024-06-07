using TravelAgency.SharedKernel.Dto.City;
using TravelAgency.SharedKernel.Dto.Hotel;

namespace TravelAgency.SharedKernel.Dto.Offer
{
    public class OfferDto
    {
        public int OfferId { get; set; }
        public HotelDto? Hotel { get; set; }
        public decimal Price { get; set; }
        public CityDto? DepartureCity { get; set; } 
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
}
