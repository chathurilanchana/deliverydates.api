using System;

namespace DeliveryDates.Api.Features.DeliveryDates.Entities
{
    public class DeliveryOption
    {
        public DeliveryOption(DateTime deliveryDate, bool isGreenDelivery)
        {
            DeliveryDate = deliveryDate;
            IsGreenDelivery = isGreenDelivery;
        }

        public DateTime DeliveryDate { get; }

        public bool IsGreenDelivery { get; }
    }
}
