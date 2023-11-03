using MediatR;
namespace CarWorkshop.Application.CarWorkshopService.Command;

public class CreateCarWorkshopServiceCommand : CarWorkshopServiceDto, IRequest
{
    public string CarWorkshopEncodedName { get; set; } = default!;
}