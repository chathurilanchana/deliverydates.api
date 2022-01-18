using System;
using System.Collections.Generic;
using System.Net;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.DeliveryDates.Models;
using DeliveryDates.Api.Middleware.ErrorHandling;

namespace DeliveryDates.Api.Features.DeliveryDates.Mappings
{
    internal static class ProductMapper
    {
        public static List<Entities.Product> ToDomain(List<GetDeliveryDatesRequest.Product> products)
        {
            var mappedProducts = new List<Entities.Product>();
            foreach (var product in products)
            {
                mappedProducts.Add(new Entities.Product
                {
                    DeliveryDays = product.DeliveryDays,
                    Name = product.Name,
                    Type = GetProductType(product.Type),
                    DaysInAdvance = product.DaysInAdvance,
                    Id = product.Id
                });
            }

            return mappedProducts;
        }

        private static ProductType GetProductType(GetDeliveryDatesRequest.Product.ProductType productType)
        {
            if (Enum.IsDefined(typeof(Entities.ProductType), productType.ToString()))
            {
                return (Entities.ProductType)Enum.Parse(typeof(Entities.ProductType), productType.ToString());
            }

            var errorMessage = $"Enum value {productType} is defined in model, but not in entities";
            throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "ApplicationError", errorMessage);
        }
    }
}
