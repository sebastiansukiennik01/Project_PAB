using TravelAgency.SharedKernel.Dto;

namespace TravelAgency.Application.Services
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
