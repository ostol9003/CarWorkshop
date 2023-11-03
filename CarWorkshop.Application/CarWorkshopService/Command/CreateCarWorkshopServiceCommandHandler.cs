using AutoMapper;
using CarWorkshop.Application.AplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshopService.Command;

public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
{
    private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;
    private readonly IMapper _mapper;
    private readonly ICarWorkshopRepository _repository;
    private readonly IUserContext _userContext;

    public CreateCarWorkshopServiceCommandHandler(ICarWorkshopRepository repository, IMapper mapper,
        IUserContext userContext, ICarWorkshopServiceRepository carWorkshopServiceRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _userContext = userContext;
        _carWorkshopServiceRepository = carWorkshopServiceRepository;
    }

    public async Task Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
    {
        var carWorkshop = await _repository.GetByEncodedName(request.CarWorkshopEncodedName);

        var user = _userContext.GetCurrentUser();
        var isEditable = user != null && (carWorkshop.CreatedById == user.Id || user.IsInRole("Moderator"));

        if (!isEditable) return;

        var carWorkshopService = new Domain.Entities.CarWorkshopService();
        _mapper.Map(request, carWorkshopService);
        carWorkshopService.CarWorkshopId = carWorkshop.Id;

        await _carWorkshopServiceRepository.Create(carWorkshopService);
    }
}