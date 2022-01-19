using System;
using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Filters;
using Xunit;

namespace DeliveryDates.Api.UnitTests.DeliveryDates.Filters
{
    public class TemporaryProductsDeliveryDatesFilterTests : DeliveryDatesTestBase
    {

        [Fact]
        public void GivenDeliveryDates_WhenPassedToFilter_OnlyWeekdaysMatchingMondayToSundayOfCurrentWeekShouldBeReturned()
        {
            //arrange
            var products = GetProducts();
            var deliveryDatesUpto2Weeks = GetUpcomming14Days();

            //act
            var sut = GetSut();
            var result = sut.GetDeliveryOptions(products, deliveryDatesUpto2Weeks);

            //assert
            Assert.NotNull(result);
            var expectedDeliveryDates = GetExpectedDeliveryDaysForTemporaryProduct();
            Assert.Equal(expectedDeliveryDates.Count, result.Count);
            foreach (var expectedDeliveryDate in expectedDeliveryDates)
            {
                Assert.Contains(expectedDeliveryDate, result);
            }
        }

        private List<DateTime> GetExpectedDeliveryDaysForTemporaryProduct()
        {
            var possibleDay = DateTime.Today;
            var deliveryDaysForTempProducts = new List<DateTime>();

            while (true)
            {
                if (possibleDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    break;
                }

                possibleDay = possibleDay.AddDays(1);
                deliveryDaysForTempProducts.Add(possibleDay);
            }

            return deliveryDaysForTempProducts;
        }

        private TemporaryProductsDeliveryDatesFilter GetSut()
        {
            return new TemporaryProductsDeliveryDatesFilter();
        }
    }
}
