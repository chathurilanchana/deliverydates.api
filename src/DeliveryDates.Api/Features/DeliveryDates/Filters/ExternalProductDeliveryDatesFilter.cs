using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Filters
{
    public class ExternalProductDeliveryDatesFilter : IDeliveryDatesFilter
    {
        public List<DateTime> GetDeliveryOptions(List<Product> products, List<DateTime> availableDeliveryDates)
        {
            if (products.Any(p => p.Type == ProductType.External))
            {
                return availableDeliveryDates.Where(p => p >= DateTime.Today.AddDays(Constants.ExternalProductDeliveryDaysInAdvance))
                    .ToList();
            }

            return availableDeliveryDates;
        }
    }
}
