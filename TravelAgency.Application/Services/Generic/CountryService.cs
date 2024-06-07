using AutoMapper;
using TravelAgency.Domain.Exceptions;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;
using TravelAgency.SharedKernel.Dto.Country;
using TravelAgency.Application.Services.Interfaces;

namespace TravelAgency.Application.Services.Generic
{
    public class CountryService : ICountryService
    {
        private readonly ITravelAgencyUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CountryService(ITravelAgencyUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public int Create(CreateCountryDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Country dto is null");
            }

            var country = _mapper.Map<Country>(dto);

            _uow.CountryRepository.Insert(country);
            _uow.Commit();

            return country.CountryId;
        }

        public void Delete(int id)
        {
            var country = _uow.CountryRepository.Get(id);

            if (country == null)
            {
                throw new NotFoundException("Country not found");
            }

            _uow.CountryRepository.Delete(country);
            _uow.Commit();
        }

        public List<CountryDto> GetAll()
        {
            var countries = _uow.CountryRepository.GetAll();
            List<CountryDto> result = _mapper.Map<List<CountryDto>>(countries);
            return result;
        }

        public CountryDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var city = _uow.CountryRepository.Get(id);
            if (city == null)
            {
                throw new NotFoundException($"Country with id {id} was not found");
            }

            var result = _mapper.Map<CountryDto>(city);
            return result;
        }

        public void Update(UpdateCountryDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No data to update");
            }

            var country = _uow.CountryRepository.Get(dto.CountryId);
            if (country == null)
            {
                throw new NotFoundException($"Country with id {dto.CountryId} was not found");
            }

            country.Name = dto.Name;
            _uow.Commit();
        }
    }
}
