using System.Collections.Generic;

namespace DeliveryDates.Api.Features.DeliveryDates.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Weekday> DeliveryDays { get; set; }
        public ProductType Type { get; set; }
        public int DaysInAdvance { get; set; }
    }
}
