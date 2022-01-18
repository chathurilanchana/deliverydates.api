using AutoFixture;
using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using System;

namespace DeliveryDates.Api.UnitTests.DeliveryDates
{
    public class DeliveryDatesTestBase
    {
        protected readonly Fixture _fixture;

        protected DeliveryDatesTestBase()
        {
            _fixture = new Fixture();
        }

        protected List<Product> GetProducts(
            List<Weekday> product1DeliveryDays = null,
            List<Weekday> product2DeliveryDays= null,
            ProductType type = ProductType.Normal,
            int daysInAdvance = 0)
        {
            return new List<Product>()
            {
                new Product(
                    1,
                    _fixture.Create<string>(),
                    product1DeliveryDays,
                    type,
                    daysInAdvance),
                new Product(
                    2,
                    _fixture.Create<string>(),
                    product2DeliveryDays,
                    type,
                    daysInAdvance),
            };
        }

        protected List<DateTime> Get14DatesFromToday()
        {
            var deliveryDates = new List<DateTime>();

            for (var i = 1; i <= 14; i++)
            {
                deliveryDates.Add(DateTime.Today.AddDays(i));
            }

            return deliveryDates;
        }
    }
}
