using FluentValidation;
using TravelAgency.Domain.Contracts;
using TravelAgency.SharedKernel.Dto.Country;

namespace TravelAgency.Application.Validators.Country
{
    public class RegisterUpdateCountryDtoValidator : AbstractValidator<UpdateCountryDto>
    {
        public RegisterUpdateCountryDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }

    }
}
