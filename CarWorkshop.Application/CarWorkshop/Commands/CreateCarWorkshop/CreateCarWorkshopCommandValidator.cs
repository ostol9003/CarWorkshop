using CarWorkshop.Domain.Interfaces;
using FluentValidation;

namespace CarWorkshop.Application.CarWorkshop.Commands.CReateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Minimum length - 2 characters")
                .MaximumLength(20).WithMessage("Maximum length - 20 characters")
                .Custom((value, context) =>
                {
                    var existingCarWorkshop = repository.GetByName(value).Result;
                    if (existingCarWorkshop != null)
                    {
                        context.AddFailure($"{value} is not unique name for car workshop");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8)
                .WithMessage("Phone number length between 8-20 characters ")
                .MaximumLength(12)
                .WithMessage("Phone number length between 8-12 characters ");
        }
    }
}
