using System;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Filters;
using Xunit;

namespace DeliveryDates.Api.UnitTests.DeliveryDates.Filters
{
    public class DaysInAdvanceDeliveryDatesFilterTests : DeliveryDatesTestBase
    {

        [Fact]
        public void GivenDeliveryDates_WhenPassedToFilter_OnlyDaysThatMatchesDaysInAdvancedShouldBeReturned()
        {
            //arrange
            const int daysInAdvance = 2;

            var products = GetProducts(daysInAdvance: daysInAdvance);
            var deliveryDatesUpto2Weeks = GetUpcomming14Days();

            //act
            var sut = GetSut();
            var result = sut.GetDeliveryOptions(products, deliveryDatesUpto2Weeks);

            //assert
            Assert.NotNull(result);
            Assert.True(result.All(d => d >= DateTime.Today.AddDays(daysInAdvance)));
        }

        private DaysInAdvanceDeliveryDatesFilter GetSut()
        {
            return new DaysInAdvanceDeliveryDatesFilter();
        }
    }
}
