using DeliveryDates.Api.Features.DeliveryDates.Models;
using FluentValidation;

namespace DeliveryDates.Api.Features.DeliveryDates.Validators
{
    public class GetDeliveryDatesRequestValidator : AbstractValidator<GetDeliveryDatesRequest>
    {
        public GetDeliveryDatesRequestValidator()
        {
            RuleFor(x => x.PostalCode).NotNull().NotEmpty().Length(5)
                .WithMessage("Postal code should be a valid swedish code");
            RuleFor(x => x.Products).NotNull().WithMessage("At least one product id should be provided");
            RuleForEach(x => x.Products).SetValidator(new ProductValidator());
        }

        class ProductValidator : AbstractValidator<GetDeliveryDatesRequest.Product>
        {
            public ProductValidator()
            {
                RuleFor(x => x.Id).NotEqual(0).WithMessage("Product id is mandatory");
                RuleFor(x => x.Name).NotEmpty().WithMessage("product name is mandatory");
                RuleFor(x => x.Type).IsInEnum().WithMessage("product type is not as expected");
                RuleFor(x => x.DeliveryDays).NotNull().WithMessage("delivery dates should not be null");
                RuleForEach(x => x.DeliveryDays).IsInEnum().WithMessage("delivery days is not in expected format");
                RuleFor(x => x.DaysInAdvance).NotEqual(0).WithMessage("days in advance is not as expected");
            }
        }
    }
}
