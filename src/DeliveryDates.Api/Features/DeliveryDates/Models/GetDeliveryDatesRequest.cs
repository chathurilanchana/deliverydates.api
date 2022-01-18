using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Entities;

namespace DeliveryDates.Api.Features.DeliveryDates.Models
{
    public class GetDeliveryDatesRequest
    {
        /// <summary>
        /// swedish postal code
        /// </summary>
        /// <example>17743</example>
        public string PostalCode { get; set; }

        /// <summary>
        /// list of products. Al least one id should be provided
        /// </summary>
        public List<Product> Products { get; set; }

        public class Product
        {
            /// <summary>
            /// Id of the product
            /// </summary>
            /// <example>100</example>
            public long Id { get; set; }

            /// <summary>
            ///Name of the product
            /// </summary>
            /// <example>Better than Tagliatelle EKO 300g</example>
            public string Name { get; set; }

            /// <summary>
            ///Possible delivery days for this product. Should be one or more of weekday
            /// </summary>
            /// <example>[Monday, Tuesday, Wednesday]</example>
            public List<Weekday> DeliveryDays { get; set; }

            /// <summary>
            ///One of Normal, External, Temporary
            /// </summary>
            /// <example>Temporary </example>
            public ProductType Type { get; set; }

            /// <summary>
            ///How many days in advance product should be ordered
            /// </summary>
            /// <example>1 </example>
            public int DaysInAdvance { get; set; }

            public enum ProductType
            {
                Normal,
                External,
                Temporary
            }
        }
    }
}
