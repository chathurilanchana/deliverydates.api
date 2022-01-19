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
        public void GivenDeliveryDates_WhenPassedToFilter_OnlyWeekdaysMatchingBothProductsShouldBeReturned()
        {
            //arrange
            const Weekday commonDayForBothProducts = Weekday.Monday;
            var product1DeliveryDays = new List<Weekday> { commonDayForBothProducts };
            var product2DeliveryDays = new List<Weekday> { commonDayForBothProducts, Weekday.Tuesday };

            var products = GetProducts(product1DeliveryDays, product2DeliveryDays);
            var deliveryDatesUpto2Weeks = GetUpcomming14Days();

            //act
            var sut = GetSut();
            var result = sut.GetDeliveryOptions(products, deliveryDatesUpto2Weeks);

            //assert
            Assert.NotNull(result);
            Assert.True(result.All(d => d.DayOfWeek == (DayOfWeek)Enum.Parse(typeof(DayOfWeek), commonDayForBothProducts.ToString())));
        }

        private ProductDeliveryDatesFilter GetSut()
        {
            return new ProductDeliveryDatesFilter();
        }
    }
}
