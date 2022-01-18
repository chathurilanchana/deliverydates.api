using System;
using System.Net;
using System.Threading.Tasks;
using DeliveryDates.Api.Features.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace DeliveryDates.Api.Middleware.ErrorHandling
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var routeData = JsonConvert.SerializeObject(context.GetRouteData().Values);
            var controllerName = context.GetRouteData().Values["controller"].ToString();
            var actionName = context.GetRouteData().Values["action"].ToString();
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var responseStatusCodeId = (int)HttpStatusCode.InternalServerError;
            var errorCode = string.Empty;
            var detailedMessage = environmentName == "Development" ? exception.ToString() : "Something went wrong.";

            if (exception is HttpStatusCodeException castedException)
            {
                responseStatusCodeId = (int)castedException.StatusCode;
                errorCode = castedException.ErrorCode;
                detailedMessage = castedException.DetailedError;

                //TODO: Change this to logWarn
                Console.WriteLine($"Controller: {controllerName} action: {actionName} routeData: {routeData}  exception: {exception}");
            }
            else
            {
                //TODO: Change this to logError
                Console.WriteLine($"Controller: {controllerName} action: {actionName} routeData: {routeData}  exception: {exception}");
            }

            await WriteResponseAsync(exception, context, responseStatusCodeId, errorCode, detailedMessage);
        }

        private async Task WriteResponseAsync(Exception ex, HttpContext context, int responseStatusCodeId, string errorCode, string detailedMessage)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = responseStatusCodeId;
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                ErrorCode = errorCode,
                ErrorMessage = detailedMessage
            }.ToString());
        }

    }
}
