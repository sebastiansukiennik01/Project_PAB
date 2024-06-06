using AutoMapper;
using TravelAgency.Domain.Exceptions;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;
using TravelAgency.SharedKernel.Dto.City;

namespace TravelAgency.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ITravelAgencyUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityService(ITravelAgencyUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CreateCityDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("City dto is null");
            }

            var city = _mapper.Map<City>(dto);

            _uow.CityRepository.Insert(city);
            _uow.Commit();

            return city.CityId;
        }

        public void Delete(int id)
        {
            var city = _uow.CityRepository.Get(id);

            if (city == null)
            {
                throw new NotFoundException("City not found");
            }

            _uow.CityRepository.Delete(city);
            _uow.Commit();
        }

        public List<CityDto> GetAll()
        {
            var cities = _uow.CityRepository.GetAll();
            List<CityDto> result = _mapper.Map<List<CityDto>>(cities);
            return result;
        }

        public CityDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var city = _uow.CityRepository.Get(id);
            if (city == null)
            {
                throw new NotFoundException($"City with id {id} was not found");
            }

            var result = _mapper.Map<CityDto>(city);
            return result;
        }

        public void Update(UpdateCityDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No data to update");
            }

            var city = _uow.CityRepository.Get(dto.CityId);
            if (city == null)
            {
                throw new NotFoundException($"City with id {dto.CityId} was not found");
            }

            city.Name = dto.Name;
            city.CountryId = dto.CountryId;

            _uow.Commit();
        }
    }
}
