using System;
using System.Collections.Generic;

namespace DeliveryDates.Api.Features.DeliveryDates.Models
{
    public class GetDeliveryDatesResponse
    {
        /// <summary>
        /// postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Possible filtered delivery dates.
        /// </summary>
        public List<DeliveryOption> DeliveryOptions { get; set; }

        public class DeliveryOption
        {
            public DateTime DeliveryDate { get; set; }

            public bool IsGreenDelivery { get; set; }
        }
    }
}
