using AutoMapper;
using TravelAgency.Domain.Exceptions;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;
using TravelAgency.SharedKernel.Dto.Hotel;

namespace TravelAgency.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly ITravelAgencyUnitOfWork _uow;
        private readonly IMapper _mapper;

        public HotelService(ITravelAgencyUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CreateHotelDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Hotel dto is null");
            }

            var hotel = _mapper.Map<Hotel>(dto);

            _uow.HotelRepository.Insert(hotel);
            _uow.Commit();

            return hotel.HotelId;
        }

        public void Delete(int id)
        {
            var hotel = _uow.HotelRepository.Get(id);

            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }

            _uow.HotelRepository.Delete(hotel);
            _uow.Commit();
        }

        public List<HotelDto> GetAll()
        {
            var cities = _uow.HotelRepository.GetAll();
            List<HotelDto> result = _mapper.Map<List<HotelDto>>(cities);
            return result;
        }

        public HotelDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var hotel = _uow.HotelRepository.Get(id);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with id {id} was not found");
            }

            var result = _mapper.Map<HotelDto>(hotel);
            return result;
        }

        public void Update(UpdateHotelDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No data to update");
            }

            var hotel = _uow.HotelRepository.Get(dto.HotelId);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with id {dto.HotelId} was not found");
            }

            hotel.Name = dto.Name;
            hotel.Rate = dto.Rate;
            hotel.CityId = dto.CityId;

            _uow.Commit();
        }
    }
}
