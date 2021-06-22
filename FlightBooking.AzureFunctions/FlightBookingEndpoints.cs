using FlightBooking.Models.RequestModels;
using FlightBooking.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace FlightBooking.AzureFunctions
{
    public partial class FlightBookingEndpoints
    {
        IMediator _mediator;

        public FlightBookingEndpoints(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetFlightBookingDetails")]
        public async Task<IActionResult> GetFlightBookingDetailsByPnr([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "getbookingdetails")]
 HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<GetBookingDetailsRequestModel>(requestBody);

            var response = await _mediator.Send<GetBookingDetailsResponseModel>(request);
            if (response == null)
                return new NotFoundResult();

            return new OkObjectResult(response);
        }

        [FunctionName("PostFlightBooking")]
        public async Task<IActionResult> PostFlightBookingDetails([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "bookflight")]
 HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<PostFlightBookingRequestModel>(requestBody);

            var response = await _mediator.Send<PostFlightBookingResponseModel>(request);
            if (response == null)
                return new NotFoundResult();

            return new OkObjectResult(response);
        }
    }
}
