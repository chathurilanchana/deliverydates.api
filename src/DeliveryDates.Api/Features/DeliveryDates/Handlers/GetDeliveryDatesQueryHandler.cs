using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.DeliveryDates.Filters;
using DeliveryDates.Api.Features.Shared.Cqrs;

namespace DeliveryDates.Api.Features.DeliveryDates.Handlers
{
    internal class GetDeliveryDatesQueryHandler : IQueryHandler<List<Product>, List<DeliveryOption>>
    {
        private const int PossibleDeliveryDays = 14;
        private const DayOfWeek GreenDeliveryDay = DayOfWeek.Thursday;
        private readonly IEnumerable<IDeliveryDatesFilter> _deliveryDatesFilters;

        public GetDeliveryDatesQueryHandler(IEnumerable<IDeliveryDatesFilter> deliveryDatesFilters)
        {
            _deliveryDatesFilters = deliveryDatesFilters;
        }

        public List<DeliveryOption> Handle(List<Product> products)
        {
            var possibleDeliveryDates = GetPossibleDeliveryDays();

            foreach (var deliveryDateFilter in _deliveryDatesFilters)
            {
                possibleDeliveryDates =
                    deliveryDateFilter.GetDeliveryOptions(products, possibleDeliveryDates);
            }

            var deliveryDatesWithGreenDelivery = DecorateWithGreenDelivery(possibleDeliveryDates);

            return SortedDeliveryDates(deliveryDatesWithGreenDelivery);
        }

        private List<DeliveryOption> SortedDeliveryDates(List<DeliveryOption> deliveryDatesWithGreenDelivery)
        {
            var sortedDeliveryDates = new List<DeliveryOption>();

            var greenDeliveryInNext3Days = deliveryDatesWithGreenDelivery.Where(d=>
                d.IsGreenDelivery && d.DeliveryDate <DateTime.Today.AddDays(3)).OrderBy(d=>d.DeliveryDate).ToList();

            var nonGreenDeliveryDates = deliveryDatesWithGreenDelivery.Except(greenDeliveryInNext3Days).OrderBy(p=>p.DeliveryDate);

            sortedDeliveryDates.AddRange(greenDeliveryInNext3Days);
            sortedDeliveryDates.AddRange(nonGreenDeliveryDates);

            return sortedDeliveryDates;
        }

        private List<DeliveryOption> DecorateWithGreenDelivery(List<DateTime> possibleDeliveryDates)
        {
            var deliveryDaysDecoratedWithGreenDelivery = new List<DeliveryOption>();

            foreach (var deliveryDate in possibleDeliveryDates)
            {
                var deliveryOption = deliveryDate.DayOfWeek == GreenDeliveryDay
                    ? new DeliveryOption(deliveryDate, true)
                    : new DeliveryOption(deliveryDate, false);

                deliveryDaysDecoratedWithGreenDelivery.Add(deliveryOption);
            }

            return deliveryDaysDecoratedWithGreenDelivery;
        }

        private List<DateTime> GetPossibleDeliveryDays()
        {
            var possibleDeliveryDates = new List<DateTime>();

            for (int i = 1; i <= PossibleDeliveryDays; i++)
            {
                possibleDeliveryDates.Add(DateTime.Today.AddDays(i));
            }

            return possibleDeliveryDates;
        }
    }
}
