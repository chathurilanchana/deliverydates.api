using AutoFixture;
using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using Models = DeliveryDates.Api.Features.DeliveryDates.Models;
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

        protected Models.GetDeliveryDatesRequest GetRequest(
            List<Weekday> product1DeliveryDays = null,
            List<Weekday> product2DeliveryDays = null,
            Models.GetDeliveryDatesRequest.Product.ProductType type = Models.GetDeliveryDatesRequest.Product.ProductType.Normal,
            int daysInAdvance = 0)
        {
            var products = new List<Models.GetDeliveryDatesRequest.Product>()
            {
                new Models.GetDeliveryDatesRequest.Product
                {
                    Id = 1,
                    Name =   _fixture.Create<string>(),
                    DeliveryDays =  product1DeliveryDays,
                    Type = type,
                    DaysInAdvance=daysInAdvance
                },
                                new Models.GetDeliveryDatesRequest.Product
                {
                    Id = 2,
                    Name =   _fixture.Create<string>(),
                    DeliveryDays = product2DeliveryDays,
                    Type = type,
                    DaysInAdvance=daysInAdvance
                }
            };

            return new Models.GetDeliveryDatesRequest { PostalCode = "123", Products = products };
        }

        protected List<Product> GetProducts(
            List<Weekday> product1DeliveryDays = null,
            List<Weekday> product2DeliveryDays = null,
            ProductType type = ProductType.Normal,
            int daysInAdvance = 0)
        {
            return new List<Product>()
            {
                new Product
                (
                    1,
                    _fixture.Create<string>(),
                    product1DeliveryDays,
                    type,
                    daysInAdvance
                ),
                                new Product
                (
                    2,
                    _fixture.Create<string>(),
                    product2DeliveryDays,
                    type,
                    daysInAdvance
                )
            };
        }

        protected List<DateTime> GetUpcomming14Days()
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
