using AutoMapper;
using TravelAgency.Domain.Models;
using TravelAgency.SharedKernel.Dto.City;
using TravelAgency.SharedKernel.Dto.Country;
using TravelAgency.SharedKernel.Dto.Hotel;
using TravelAgency.SharedKernel.Dto.Offer;

namespace TravelAgency.Application.Mappings
{
    public class TravelAgencyMappingProfile : Profile
    {
        public TravelAgencyMappingProfile()
        {
            // mappings for city
            CreateMap<City, CityDto>()
                .ForMember(
                dto => dto.Country,
                           opt => opt.MapFrom(
                               c => new CountryDto()
                               {
                                   CountryId = c.Country.CountryId,
                                   Name = c.Country.Name
                               }
                               )
                           );

            CreateMap<CreateCityDto, City>()
                .ForMember(c => c.Country, opt => opt.Ignore())
                .ForMember(c => c.CountryId, opt => opt.MapFrom(dto => dto.CountryId));

            CreateMap<UpdateCityDto, City>()
                .ForMember(c => c.Country, opt => opt.Ignore())
                .ForMember(c => c.CountryId, opt => opt.MapFrom(dto => dto.CountryId));

            // mappings for Country
            CreateMap<Country, CountryDto>();
            CreateMap<CreateCountryDto, Country>();
            CreateMap<UpdateCountryDto, Country>();

            // mappings for Hotel
            CreateMap<Hotel, HotelDto>()
                .ForMember(
                    dto => dto.City,
                    opt => opt.MapFrom(
                        h => new CityDto()
                        {
                            CityId = h.CityId,
                            Name = h.City.Name,
                            Country = new CountryDto()
                            {
                                CountryId = h.City.CountryId,
                                Name = h.City.Country.Name
                            }
                        }
                    )
                );

            CreateMap<CreateHotelDto, Hotel>()
                .ForMember(h => h.City, opt => opt.Ignore())
                .ForMember(h => h.CityId, opt => opt.MapFrom(dto => dto.CityId));

            CreateMap<UpdateHotelDto, Hotel>()
                .ForMember(h => h.City, opt => opt.Ignore())
                .ForMember(h => h.CityId, opt => opt.MapFrom(dto => dto.CityId));

            // mappings for Offer
            CreateMap<Offer, OfferDto>()
                .ForMember(
                dto => dto.DepartureCity,
                opt => opt.MapFrom(
                    h => new CityDto()
                    {
                        CityId = h.DepartureCityId,
                        Name = h.DepartureCity.Name,
                        Country = new CountryDto()
                        {
                            CountryId = h.DepartureCity.CountryId,
                            Name = h.DepartureCity.Country.Name
                        }
                    }
                    )
                )
                .ForMember(
                dto => dto.Hotel,
                opt => opt.MapFrom(
                    o => new HotelDto()
                    {
                        HotelId = o.HotelId,
                        Name = o.Hotel.Name,
                        Rate = o.Hotel.Rate,
                        City = new CityDto()
                        {
                            CityId = o.Hotel.CityId,
                            Name = o.Hotel.City.Name,
                            Country = new CountryDto()
                            {
                                CountryId = o.Hotel.City.CountryId,
                                Name = o.Hotel.City.Country.Name
                            }
                        }
                    }
                    ));

            CreateMap<CreateOfferDto, Offer>()
                .ForMember(o => o.DepartureCity, opt => opt.Ignore())
                .ForMember(o => o.DepartureCityId, opt => opt.MapFrom(dto => dto.DepartureCityId))
                .ForMember(o => o.Hotel, opt => opt.Ignore())
                .ForMember(o => o.HotelId, opt => opt.MapFrom(dto => dto.HotelId));
            
            CreateMap<UpdateOfferDto, Offer>()
                .ForMember(o => o.DepartureCity, opt => opt.Ignore())
                .ForMember(o => o.DepartureCityId, opt => opt.MapFrom(dto => dto.DepartureCityId))
                .ForMember(o => o.Hotel, opt => opt.Ignore())
                .ForMember(o => o.HotelId, opt => opt.MapFrom(dto => dto.HotelId));
        }

    }
}

