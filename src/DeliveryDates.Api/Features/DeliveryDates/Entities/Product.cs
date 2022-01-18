using System.Collections.Generic;

namespace DeliveryDates.Api.Features.DeliveryDates.Entities
{
    public class Product
    {
        public Product(long id, string name, List<Weekday> deliveryDays, ProductType type, int daysInAdvance)
        {
            Id = id;
            Name = name;
            DeliveryDays = deliveryDays;
            Type = type;
            DaysInAdvance = daysInAdvance;
        }

        public long Id { get; }
        public string Name { get; }
        public List<Weekday> DeliveryDays { get; }
        public ProductType Type { get; }
        public int DaysInAdvance { get; }
    }
}
