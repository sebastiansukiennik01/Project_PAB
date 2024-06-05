using FluentValidation;
using TravelAgency.Domain.Contracts;
using TravelAgency.SharedKernel.Dto;

namespace TravelAgency.Application.Validators
{
    public class RegisterUpdateCityDtoValidator : AbstractValidator<UpdateCityDto>
    {
        public RegisterUpdateCityDtoValidator(ITravelAgencyUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(c => c.CountryId)
                .Custom((value, context) =>
                {
                    bool exists = unitOfWork.CityRepository.CountryExists(value);
                    if (!exists)
                    {
                        context.AddFailure("CountryId", $"City of id {value} does not exist");
                    }
                });
        }

    }
}
