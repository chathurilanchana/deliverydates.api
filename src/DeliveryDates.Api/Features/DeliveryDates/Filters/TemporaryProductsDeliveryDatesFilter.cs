using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Filters
{
    internal class TemporaryProductsDeliveryDatesFilter : IDeliveryDatesFilter
    {
        public List<DateTime> GetDeliveryOptions(List<Product> products, List<DateTime> availableDeliveryDates)
        {
            if (products.Any(p => p.Type == ProductType.Temporary))
            {
                var remainingDaysOfCurrentWeek = 7 - (DateTime.Today.DayOfWeek - DayOfWeek.Sunday);

                return availableDeliveryDates
                    .Where(p => p <= DateTime.Today.AddDays(remainingDaysOfCurrentWeek))
                    .ToList();
            }

            return availableDeliveryDates;
        }
    }
}
