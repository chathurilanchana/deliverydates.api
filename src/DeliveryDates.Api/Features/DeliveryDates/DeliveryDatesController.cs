using System.Net;
using DeliveryDates.Api.Features.DeliveryDates.Models;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace DeliveryDates.Api.Features.DeliveryDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryDatesController : ControllerBase
    {
        private readonly IDeliveryDatesService _deliveryDatesService;

        public DeliveryDatesController(IDeliveryDatesService deliveryDatesService)
        {
            _deliveryDatesService = deliveryDatesService;
        }

        //Note: Method was defined as a post, due to heavy data that we need to pass to the endpoint
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(GetDeliveryDatesResponse))]
        public IActionResult Post([FromBody] GetDeliveryDatesRequest getDeliveryDatesRequest)
        {
            return Ok(_deliveryDatesService.GetDeliveryDatesAsync(getDeliveryDatesRequest));
        }
    }
}
