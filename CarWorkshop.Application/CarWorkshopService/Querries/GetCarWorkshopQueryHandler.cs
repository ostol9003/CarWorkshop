using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshopService.Querries
{
    internal class GetCarWorkshopQueryHandler : IRequestHandler<GetCarWorkshopServiceQuery, IEnumerable<CarWorkshopServiceDto>>
    {
        private readonly ICarWorkshopServiceRepository _repository;
        private readonly IMapper _mapper;

        public GetCarWorkshopQueryHandler(ICarWorkshopServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarWorkshopServiceDto>> Handle(GetCarWorkshopServiceQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllByEncodedName(request.EncodedName);
            var dtos = _mapper.Map<IEnumerable<CarWorkshopServiceDto>>(result);
            return dtos;
        }
    }
}
