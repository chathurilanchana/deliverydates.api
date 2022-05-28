using System;
using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.DeliveryDates.Models;
using DeliveryDates.Api.Features.Shared.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryDates.Api.Features.DeliveryDates
{
    public interface IDeliveryDatesService
    {
        GetDeliveryDatesResponse GetDeliveryDatesAsync(GetDeliveryDatesRequest request);
    }
    internal class DeliveryDatesService : IDeliveryDatesService
    {
        private readonly IServiceProvider _serviceProvider;

        public DeliveryDatesService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public GetDeliveryDatesResponse GetDeliveryDatesAsync(GetDeliveryDatesRequest request)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<GetDeliveryDatesRequest,GetDeliveryDatesResponse>>();
            return handler.Handle(request);
        }
    }
}
