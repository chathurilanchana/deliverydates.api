using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryDates.Api.Features.DeliveryDates.Entities
{
    public class DeliveryOptions
    {
        private IEnumerable<DeliveryOption> Options;

        public DeliveryOptions(List<DateTime> deliveryDates)
        {
            Options = deliveryDates.Select(p=>new DeliveryOption(p, false));
        }

        public IEnumerable<DeliveryOption> GetDeliveryOptions()
        {
            return Options;
        }

        public void DecorateWithGreenDelivery()
        {
            var deliveryDaysDecoratedWithGreenDelivery = new List<DeliveryOption>();

            foreach (var option in Options)
            {
                var deliveryOption = option.DeliveryDate.DayOfWeek == Constants.GreenDeliveryDay
                    ? new DeliveryOption(option.DeliveryDate, true)
                    : new DeliveryOption(option.DeliveryDate, false);

                deliveryDaysDecoratedWithGreenDelivery.Add(deliveryOption);
            }

            Options = deliveryDaysDecoratedWithGreenDelivery;
        }

        public void SortByDate()
        {
            var sortedDeliveryDates = new List<DeliveryOption>();

            var greenDeliveryInNext3Days = Options.Where(d =>
                d.IsGreenDelivery && d.DeliveryDate <= DateTime.Today.AddDays(3)).OrderBy(d => d.DeliveryDate).ToList();

            var nonGreenDeliveryDates = Options.Except(greenDeliveryInNext3Days).OrderBy(p => p.DeliveryDate);

            sortedDeliveryDates.AddRange(greenDeliveryInNext3Days);
            sortedDeliveryDates.AddRange(nonGreenDeliveryDates);

            Options = sortedDeliveryDates;
        }
    }
}
