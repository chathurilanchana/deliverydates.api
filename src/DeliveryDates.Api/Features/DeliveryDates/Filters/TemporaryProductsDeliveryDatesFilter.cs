using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Filters
{
    public class TemporaryProductsDeliveryDatesFilter : IDeliveryDatesFilter
    {
        public List<DateTime> GetDeliveryOptions(List<Product> products, List<DateTime> availableDeliveryDates)
        {
            var remainingDaysOfCurrentWeek = 7 - (DateTime.Today.DayOfWeek - DayOfWeek.Sunday);

            return availableDeliveryDates
                .Where(p => p <= DateTime.Today.AddDays(remainingDaysOfCurrentWeek))
                .ToList();
        }
    }
}
