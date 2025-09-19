using AutoMapper;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.ViewModels.Company;
using SchoolPortal_API.ViewModels.Country;
using SchoolPortal_API.ViewModels.State;
using SchoolPortal_API.ViewModels.City;

namespace SchoolPortal_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Company mappings
            CreateMap<CompanyMaster, CompanyResponseDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.StateName))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.CityName))
                .ForMember(dest => dest.JurisdictionAreaId, opt => opt.MapFrom(src => src.JudistrictionArea))
                .ForMember(dest => dest.JurisdictionAreaName, opt => opt.MapFrom(src => src.JudistrictionAreaNavigation.CityName));

            CreateMap<CompanyDto, CompanyMaster>()
                .ForMember(dest => dest.JudistrictionArea, opt => opt.MapFrom(src => src.JurisdictionAreaId));

            // Country mappings
            CreateMap<CountryMaster, CountryResponseDto>();
            CreateMap<CountryDto, CountryMaster>();

            // State mappings
            CreateMap<StateMaster, StateResponseDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName));
            CreateMap<StateDto, StateMaster>();

            // City mappings
            CreateMap<CityMaster, CityResponseDto>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.CityStateNavigation.StateName))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.CityStateNavigation.Country.CountryName));
            CreateMap<CityDto, CityMaster>();
        }
    }
}
