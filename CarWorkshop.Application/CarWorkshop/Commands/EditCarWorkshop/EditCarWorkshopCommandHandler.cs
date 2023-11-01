using AutoMapper;
using CarWorkshop.Application.AplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository, IMapper mapper, IUserContext userContext)
        {
            _carWorkshopRepository = carWorkshopRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.EncodedName);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && carWorkshop.CreatedById == user.Id;
            
            if (!isEditable)
            {
                return;
            }
            _mapper.Map(request, carWorkshop);


            //carWorkshop.Description = request.Description;
            //carWorkshop.About = request.About;
            //carWorkshop.ContactDetails.City = request.City;
            //carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
            //carWorkshop.ContactDetails.PostalCode = request.PostalCode;
            //carWorkshop.ContactDetails.Street = request.Street;

            await _carWorkshopRepository.Commit();

        }
    }
}
