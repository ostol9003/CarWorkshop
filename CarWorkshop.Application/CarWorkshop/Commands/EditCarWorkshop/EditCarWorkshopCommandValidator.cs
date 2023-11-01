using CarWorkshop.Domain.Interfaces;
using FluentValidation;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandValidator : AbstractValidator<EditCarWorkshopCommand>
    {
        public EditCarWorkshopCommandValidator()
        {
            RuleFor(e => e.Description)
                .NotEmpty()
                .WithMessage("Description must not be empty");

            RuleFor(e => e.PhoneNumber)
                .MinimumLength(8).WithMessage("Minimum length 8 characters")
                .MaximumLength(12).WithMessage("Maximum length 12 characters");
        }
    }
}
