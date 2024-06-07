using TravelAgency.SharedKernel.Dto.Offer;

namespace TravelAgency.Application.Services.Interfaces
{
    public interface IOfferService
    {
        List<OfferDto> GetAll();
        OfferDto GetById(int id);
        int Create(CreateOfferDto dto);
        void Update(UpdateOfferDto dto);
        void Delete(int id);
    }
}
