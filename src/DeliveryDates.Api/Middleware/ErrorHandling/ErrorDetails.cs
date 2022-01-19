using Newtonsoft.Json;

namespace DeliveryDates.Api.Middleware.ErrorHandling
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
