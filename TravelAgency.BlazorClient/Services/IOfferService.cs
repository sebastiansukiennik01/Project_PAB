using TravelAgency.SharedKernel.Dto.Offer;

namespace TravelAgency.BlazorClient.Services
{
    public interface IOfferService
    {
        Task<List<OfferDto>> GetAll();
    }
}
