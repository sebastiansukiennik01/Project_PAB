using FluentValidation;
using TravelAgency.Domain.Contracts;
using TravelAgency.SharedKernel.Dto.Offer;

namespace TravelAgency.Application.Validators.Offer
{
    public class RegisterCreateOfferDtoValidator : AbstractValidator<CreateOfferDto>
    {
        private int _max_days = 30;
        public RegisterCreateOfferDtoValidator(ITravelAgencyUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Price)
                .GreaterThan(1)
                .LessThan(100000);

            RuleFor(p => DateOnly.FromDateTime(p.DateFrom))
                .InclusiveBetween(DateOnly.FromDateTime(DateTime.Today.AddDays(1)), DateOnly.FromDateTime(DateTime.Today.AddDays(365)));

            RuleFor(p => DateOnly.FromDateTime(p.DateTo))
               .InclusiveBetween(DateOnly.FromDateTime(DateTime.Today.AddDays(1)), DateOnly.FromDateTime(DateTime.Today.AddDays(365 + _max_days)));

            // max duration of the trip
            RuleFor(p => p)
                .Custom((value, context) =>
                {
                    int duration = value.DateTo.Subtract(value.DateFrom).Days;
                    bool too_long = duration <= _max_days;
                    if (!too_long)
                    {
                        context.AddFailure("DateTo", $"Max durataion of the offer is {_max_days} days, got {duration} days");
                    }
                });

            RuleFor(p => p)
                .Custom((value, context) =>
                {
                    bool invalid = DateOnly.FromDateTime(value.DateTo) < DateOnly.FromDateTime(value.DateFrom);
                    if (invalid)
                    {
                        context.AddFailure("DateTo", $"Trying to set DateTo after DateFrom");
                    }
                });

            RuleFor(p => p.DepartureCityId)
                .Custom((value, context) =>
                {
                    bool exists = unitOfWork.CityRepository.CityExists(value);
                    if (!exists)
                    {
                        context.AddFailure("CityId", $"City of id {value} does not exist");
                    }
                });

            RuleFor(p => p.HotelId)
                .Custom((value, context) =>
                {
                    bool exists = unitOfWork.HotelRepository.HotelExists(value);
                    if (!exists)
                    {
                        context.AddFailure("HotelId", $"Hotel of id {value} does not exist");
                    }
                });
        }

    }
}
