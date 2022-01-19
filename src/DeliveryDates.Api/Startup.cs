using System.Collections.Generic;
using DeliveryDates.Api.Features.DeliveryDates;
using DeliveryDates.Api.Features.DeliveryDates.Entities;
using DeliveryDates.Api.Features.DeliveryDates.Filters;
using DeliveryDates.Api.Features.DeliveryDates.Handlers;
using DeliveryDates.Api.Features.DeliveryDates.Validators;
using DeliveryDates.Api.Features.Shared.Cqrs;
using DeliveryDates.Api.Middleware.ErrorHandling;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DeliveryDates.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter());
                    o.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetDeliveryDatesRequestValidator>());
           
            services.AddRouting();

            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "DeliveryDates API";
                };
            });

            services.AddTransient<ErrorHandlingMiddleware>();
            services.AddTransient<IDeliveryDatesService, DeliveryDatesService>();
            services.AddTransient<IQueryHandler<List<Product>, List<DeliveryOption>>, GetDeliveryDatesQueryHandler>();
            services.AddTransient<IDeliveryDatesFilter, ProductDeliveryDatesFilter>();
            services.AddTransient<IDeliveryDatesFilter, DaysInAdvanceDeliveryDatesFilter>();
            services.AddTransient<IDeliveryDatesFilter, ExternalProductDeliveryDatesFilter>();
            services.AddTransient<IDeliveryDatesFilter, TemporaryProductsDeliveryDatesFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorHandlingMiddleware();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
