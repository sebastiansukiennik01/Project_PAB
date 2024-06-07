using TravelAgency.SharedKernel.Dto.City;

namespace TravelAgency.Application.Services.Interfaces
{
    public interface ICityService
    {
        List<CityDto> GetAll();
        CityDto GetById(int id);
        int Create(CreateCityDto dto);
        void Update(UpdateCityDto dto);
        void Delete(int id);
    }
}
