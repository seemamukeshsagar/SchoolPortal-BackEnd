using AutoMapper;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.ViewModels.Company;

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
        }
    }
}
