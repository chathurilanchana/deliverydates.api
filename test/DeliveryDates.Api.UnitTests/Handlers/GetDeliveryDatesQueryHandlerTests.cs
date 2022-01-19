using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.DeliveryDates.Filters;
using DeliveryDates.Api.Features.DeliveryDates.Handlers;
using DeliveryDates.Api.UnitTests.DeliveryDates;
using Moq;
using Xunit;

namespace DeliveryDates.Api.UnitTests.Handlers
{
    public class GetDeliveryDatesQueryHandlerTests : DeliveryDatesTestBase
    {
        private readonly Mock<IDeliveryDatesFilter> _deliveryDatesFilter;

        public GetDeliveryDatesQueryHandlerTests()
        {
            _deliveryDatesFilter = new Mock<IDeliveryDatesFilter>();
        }

        [Fact]
        public void GivenListOfProducts_WhenPassedToHandler_FilteredResultsAreMarkedWithGreenDelivery()
        {
            //arrange
            var products = GetProducts();
            var deliveryDatesUpto2Weeks = GetUpcomming14Days();

            _deliveryDatesFilter.Setup(p => p.GetDeliveryOptions(It.IsAny<List<Product>>(), It.IsAny<List<DateTime>>()))
                .Returns(deliveryDatesUpto2Weeks);

            //act
            var sut = GetSut();
            var result = sut.Handle(products);

            //assert
            var expectedGreenDeliveryDates = deliveryDatesUpto2Weeks.Where(p => p.DayOfWeek == DayOfWeek.Friday).ToList();
            Assert.NotNull(result);
            Assert.Equal(deliveryDatesUpto2Weeks.Count, result.Count);
            var actualGreenDeliveryDates = result.Where(p => p.IsGreenDelivery).ToList();
            Assert.Equal(expectedGreenDeliveryDates.Count, actualGreenDeliveryDates.Count());
            foreach (var expectedGreenDeliveryDate in expectedGreenDeliveryDates)
            {
                Assert.Contains(actualGreenDeliveryDates, p => p.DeliveryDate == expectedGreenDeliveryDate);
            }
        }

        [Fact]
        public void GivenListOfProducts_WhenPassedToHandler_ResultAreSortedAsExpected()
        {
            //arrange
            var products = GetProducts();
            var deliveryDatesUpto2Weeks = GetUpcomming14Days();

            _deliveryDatesFilter.Setup(p => p.GetDeliveryOptions(It.IsAny<List<Product>>(), It.IsAny<List<DateTime>>()))
                .Returns(deliveryDatesUpto2Weeks);

            //act
            var sut = GetSut();
            var result = sut.Handle(products);

            //assert
            var expectedDateList = GetExpectedDeliveryDatesList(deliveryDatesUpto2Weeks);
            var returnedDateList = result.Select(p => p.DeliveryDate).ToList();
            Assert.True(expectedDateList.SequenceEqual(returnedDateList));
        }

        private List<DateTime> GetExpectedDeliveryDatesList(List<DateTime> deliveryDatesUpto2Weeks)
        {
            deliveryDatesUpto2Weeks.Sort();

            var sortedDeliveryDays = deliveryDatesUpto2Weeks.Where(deliveryDate => deliveryDate.DayOfWeek == Constants.GreenDeliveryDay && deliveryDate <= DateTime.Today.AddDays(3)).ToList();

            var remainingDeliveryDays = deliveryDatesUpto2Weeks.Except(sortedDeliveryDays).ToList();
            remainingDeliveryDays.Sort();

            return sortedDeliveryDays.Union(remainingDeliveryDays).ToList();
        }

        private GetDeliveryDatesQueryHandler GetSut()
        {
            return new GetDeliveryDatesQueryHandler(new List<IDeliveryDatesFilter> { _deliveryDatesFilter.Object });
        }
    }
}
