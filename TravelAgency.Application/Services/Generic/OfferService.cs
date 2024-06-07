using AutoMapper;
using TravelAgency.Domain.Exceptions;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;
using TravelAgency.SharedKernel.Dto.Offer;
using TravelAgency.Application.Services.Interfaces;

namespace TravelAgency.Application.Services.Generic
{
    public class OfferService : IOfferService
    {
        private readonly ITravelAgencyUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OfferService(ITravelAgencyUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public int Create(CreateOfferDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Offer dto is null");
            }

            var offer = _mapper.Map<Offer>(dto);

            _uow.OfferRepository.Insert(offer);
            _uow.Commit();

            return offer.OfferId;
        }

        public void Delete(int id)
        {
            var offer = _uow.OfferRepository.Get(id);

            if (offer == null)
            {
                throw new NotFoundException("Offer not found");
            }

            _uow.OfferRepository.Delete(offer);
            _uow.Commit();
        }

        public List<OfferDto> GetAll()
        {
            var offers = _uow.OfferRepository.GetAll();
            List<OfferDto> result = _mapper.Map<List<OfferDto>>(offers);
            return result;
        }

        public OfferDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var offer = _uow.OfferRepository.Get(id);
            if (offer == null)
            {
                throw new NotFoundException($"Offer with id {id} was not found");
            }

            var result = _mapper.Map<OfferDto>(offer);
            return result;
        }

        public void Update(UpdateOfferDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No data to update");
            }

            var offer = _uow.OfferRepository.Get(dto.OfferId);
            if (offer == null)
            {
                throw new NotFoundException($"Offer with id {dto.OfferId} was not found");
            }

            offer.Price = dto.Price;
            offer.DepartureCityId = dto.DepartureCityId;
            offer.HotelId = dto.HotelId;
            offer.DateFrom = dto.DateFrom;
            offer.DateTo = dto.DateTo;

            _uow.Commit();
        }
    }
}
