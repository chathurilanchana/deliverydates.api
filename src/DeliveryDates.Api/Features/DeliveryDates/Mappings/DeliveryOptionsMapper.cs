using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Models;

namespace DeliveryDates.Api.Features.DeliveryDates.Mappings
{
    internal static class DeliveryOptionsMapper
    {
        public static GetDeliveryDatesResponse ToModel(string postalCode, List<Entities.DeliveryOption> deliveryOptions)
        {
            var mappedDeliveryOptions = new List<GetDeliveryDatesResponse.DeliveryOption>();

            foreach (var deliveryOption in deliveryOptions)
            {
                mappedDeliveryOptions.Add(new GetDeliveryDatesResponse.DeliveryOption
                {
                    DeliveryDate = deliveryOption.DeliveryDate,
                    IsGreenDelivery = deliveryOption.IsGreenDelivery
                });
            }
            return new GetDeliveryDatesResponse
            {
                PostalCode = postalCode,
                DeliveryOptions = mappedDeliveryOptions
            };
        }
    }
}
