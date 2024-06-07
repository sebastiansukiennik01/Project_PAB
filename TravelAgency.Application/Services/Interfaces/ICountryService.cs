using TravelAgency.SharedKernel.Dto.Country;

namespace TravelAgency.Application.Services.Interfaces
{
    public interface ICountryService
    {
        List<CountryDto> GetAll();
        CountryDto GetById(int id);
        int Create(CreateCountryDto dto);
        void Update(UpdateCountryDto dto);
        void Delete(int id);
    }
}
