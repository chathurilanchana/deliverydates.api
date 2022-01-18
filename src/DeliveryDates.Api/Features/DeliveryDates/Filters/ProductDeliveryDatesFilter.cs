using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Filters
{
    internal class ProductDeliveryDatesFilter : IDeliveryDatesFilter
    {
        public List<DateTime> GetDeliveryOptions(List<Product> products, List<DateTime> availableDeliveryDates)
        {
            var filteredDeliveryDates = new List<DateTime>();
            var matchingWeekdaysForProducts = GetMatchingWeekdaysForProducts(products);

            foreach (var deliveryDate in availableDeliveryDates)
            {
                foreach (var matchingWeekday in matchingWeekdaysForProducts)
                {
                    if (deliveryDate.DayOfWeek.ToString().Equals(matchingWeekday.ToString(),
                            StringComparison.OrdinalIgnoreCase))
                    {
                        filteredDeliveryDates.Add(deliveryDate);
                    }
                }
            }

            return filteredDeliveryDates;
        }

        private IEnumerable<Weekday> GetMatchingWeekdaysForProducts(List<Product> products)
        {
            IEnumerable<Weekday> filteredWeekdays = new List<Weekday>()
            {
                Weekday.Monday, Weekday.Tuesday, Weekday.Wednesday, Weekday.Thursday, Weekday.Friday
            };

            foreach (var product in products)
            {
                filteredWeekdays = filteredWeekdays.Intersect(product.DeliveryDays);

                if (!filteredWeekdays.Any())
                {
                    return new List<Weekday>();
                }
            }

            return filteredWeekdays;
        }
    }
}
