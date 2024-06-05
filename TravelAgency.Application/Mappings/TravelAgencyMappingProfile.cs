using AutoMapper;
using TravelAgency.Domain.Models;
using TravelAgency.SharedKernel.Dto;

namespace TravelAgency.Application.Mappings
{
    public class TravelAgencyMappingProfile : Profile
    {
        public TravelAgencyMappingProfile()
        {
            CreateMap<City, CityDto>()
                .ForMember(dto => dto.Country,
                           opt => opt.MapFrom(c => new CountryDto { CountryId = c.Country.CountryId, Name = c.Country.Name }));
            // CreateCityDto to City mapping
            CreateMap<CreateCityDto, City>()
                .ForMember(c => c.Country, opt => opt.Ignore()) // Ignore Country property during creation
                .ForMember(c => c.CountryId, opt => opt.MapFrom(dto => dto.CountryId));

            // UpdateCityDto to City mapping
            CreateMap<UpdateCityDto, City>()
                .ForMember(c => c.Country, opt => opt.Ignore()) // Ignore Country property during update
                .ForMember(c => c.CountryId, opt => opt.MapFrom(dto => dto.CountryId));

        }

    }
}

