using TravelAgency.SharedKernel.Dto.Hotel;

namespace TravelAgency.Application.Services.Interfaces
{
    public interface IHotelService
    {
        List<HotelDto> GetAll();
        HotelDto GetById(int id);
        int Create(CreateHotelDto dto);
        void Update(UpdateHotelDto dto);
        void Delete(int id);
    }
}
