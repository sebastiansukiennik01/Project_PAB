using FluentValidation;
using TravelAgency.Domain.Contracts;
using TravelAgency.SharedKernel.Dto.Country;

namespace TravelAgency.Application.Validators.Country
{
    public class RegisterCreateCountryDtoValidator : AbstractValidator<CreateCountryDto>
    {
        public RegisterCreateCountryDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }

    }
}
