using System;
using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.Shared.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryDates.Api.Features.DeliveryDates
{
    public interface IDeliveryDatesService
    {
        List<DeliveryOption> GetDeliveryDatesAsync(List<Product> products);
    }
    internal class DeliveryDatesService : IDeliveryDatesService
    {
        private readonly IServiceProvider _serviceProvider;

        public DeliveryDatesService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<DeliveryOption> GetDeliveryDatesAsync(List<Product> products)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<List<Product>, List<DeliveryOption>>>();
            return handler.Handle(products);
        }
    }
}
