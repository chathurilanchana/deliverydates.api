using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Filters
{
    internal class DaysInAdvanceDeliveryDatesFilter : IDeliveryDatesFilter
    {
        public List<DateTime> GetDeliveryOptions(List<Product> products, List<DateTime> availableDeliveryDates)
        {
            if (!availableDeliveryDates.Any())
            {
                return availableDeliveryDates;
            }

            var maxDaysInAdvance = products.Max(p => p.DaysInAdvance);

            return availableDeliveryDates.Where(p => p >= DateTime.Today.AddDays(maxDaysInAdvance))
                .ToList();
        }
    }
}
