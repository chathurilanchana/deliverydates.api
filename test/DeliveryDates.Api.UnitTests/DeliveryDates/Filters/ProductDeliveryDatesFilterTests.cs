using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.DeliveryDates.Filters;
using Xunit;

namespace DeliveryDates.Api.UnitTests.DeliveryDates.Filters
{
    public class ProductDeliveryDatesFilterTests : DeliveryDatesTestBase
    {

        [Fact]
        public void GivenDeliveryDates_WhenPassedToFilter_OnlyDatesAfterDaysInAdvanceShouldAppear()
        {
            var product1DeliveryDays = new List<Weekday> { Weekday.Monday };
            var product2DeliveryDays = new List<Weekday> { Weekday.Monday, Weekday.Tuesday };

            var products = GetProducts(product1DeliveryDays, product2DeliveryDays);
            var deliveryDatesUpto2Weeks = Get14DatesFromToday();

            var sut = GetSut();
            var result = sut.GetDeliveryOptions(products, deliveryDatesUpto2Weeks);

            result.All(d => d.DayOfWeek == DayOfWeek.Monday);
        }

        private ProductDeliveryDatesFilter GetSut()
        {
            return new ProductDeliveryDatesFilter();
        }
    }
}
