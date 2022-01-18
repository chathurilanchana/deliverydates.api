using System;
using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Filters
{
    public interface IDeliveryDatesFilter
    {
        List<DateTime> GetDeliveryOptions(List<Product> products, List<DateTime> availableDeliveryDates);
    }
}
