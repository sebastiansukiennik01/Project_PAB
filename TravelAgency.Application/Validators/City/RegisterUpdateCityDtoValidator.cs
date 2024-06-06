using FluentValidation;
using TravelAgency.Domain.Contracts;
using TravelAgency.SharedKernel.Dto.City;

namespace TravelAgency.Application.Validators.City
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
                        context.AddFailure("CountryId", $"Country of id {value} does not exist");
                    }
                });
        }

    }
}
