using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.DeliveryDates.Filters;
using DeliveryDates.Api.Features.DeliveryDates.Mappings;
using DeliveryDates.Api.Features.DeliveryDates.Models;
using DeliveryDates.Api.Features.Shared.Cqrs;

namespace DeliveryDates.Api.Features.DeliveryDates.Handlers
{
    internal class GetDeliveryDatesQueryHandler : IQueryHandler<GetDeliveryDatesRequest, GetDeliveryDatesResponse>
    {
        private readonly IEnumerable<IDeliveryDatesFilter> _deliveryDatesFilters;

        public GetDeliveryDatesQueryHandler(IEnumerable<IDeliveryDatesFilter> deliveryDatesFilters)
        {
            _deliveryDatesFilters = deliveryDatesFilters;
        }

        public GetDeliveryDatesResponse Handle(GetDeliveryDatesRequest deliveryDatesRequest)
        {
            var possibleDeliveryDates = GetPossibleDeliveryDays();

            var products = ProductMapper.ToDomain(deliveryDatesRequest.Products);
            foreach (var deliveryDateFilter in _deliveryDatesFilters)
            {
                possibleDeliveryDates =
                    deliveryDateFilter.GetDeliveryOptions(products, possibleDeliveryDates);
            }
           
            var deliveryOptions = new DeliveryOptions(possibleDeliveryDates);
            deliveryOptions.DecorateWithGreenDelivery();
            deliveryOptions.SortByDate();

            var mappedResponse = DeliveryOptionsMapper.ToModel(deliveryDatesRequest.PostalCode, deliveryOptions);
            return mappedResponse;
        }


        private List<DateTime> GetPossibleDeliveryDays()
        {
            var possibleDeliveryDates = new List<DateTime>();

            for (int i = 1; i <= Constants.PossibleDeliveryDays; i++)
            {
                possibleDeliveryDates.Add(DateTime.Today.AddDays(i));
            }

            return possibleDeliveryDates;
        }
    }
}
