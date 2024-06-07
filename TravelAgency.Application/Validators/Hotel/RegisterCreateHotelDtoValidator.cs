using FluentValidation;
using TravelAgency.Domain.Contracts;
using TravelAgency.SharedKernel.Dto.Hotel;

namespace TravelAgency.Application.Validators.Hotel
{
    public class RegisterCreateHotelDtoValidator : AbstractValidator<CreateHotelDto>
    {
        public RegisterCreateHotelDtoValidator(ITravelAgencyUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(p => p.Rate)
                .InclusiveBetween(1, 5);

            RuleFor(p => p.CityId)
                .Custom((value, context) =>
                {
                    bool exists = unitOfWork.CityRepository.CityExists(value);
                    if (!exists)
                    {
                        context.AddFailure("CityId", $"City of id {value} does not exist");
                    }
                });
        }

    }
}
