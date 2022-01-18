using System;

namespace DeliveryDates.Api.Features.DeliveryDates.Entities
{
    public class DeliveryOption
    {
        public DateTime DeliveryDate { get; set; }

        public bool IsGreenDelivery { get; set; }
    }
}
