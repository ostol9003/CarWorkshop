using AutoMapper;
using CarWorkshop.Application.AplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CReateCarWorkshop;
using CarWorkshop.Application.CarWorkshopService;
using CarWorkshop.Application.CarWorkshopService.Command;

namespace CarWorkshop.Application.Mappings;

public class CarWorkshopServiceMappingProfile : Profile
{
    public CarWorkshopServiceMappingProfile(IUserContext userContext)
    {
        CreateMap<CarWorkshopServiceDto, Domain.Entities.CarWorkshopService>()
            .ReverseMap();

        CreateMap<CreateCarWorkshopServiceCommand, Domain.Entities.CarWorkshopService>()
            .ForMember(dst => dst.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description));

    }
    
}