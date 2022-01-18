using System;
using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Filters
{
    interface IDeliveryDatesFilter
    {
        public List<DateTime> GetDeliveryOptions(List<Product> products,
            List<DateTime> availableDeliveryDates);
    }
}
