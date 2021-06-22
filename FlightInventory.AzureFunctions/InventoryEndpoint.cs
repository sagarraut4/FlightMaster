// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using FlightBookingSystem.Common.Helpers;
using FlightInventory.Models.RequestModels;
using FlightInventory.Models.ResponseModels;
using MediatR;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FlightInventory.AzureFunctions
{
    public class InventoryEndpoint
    {
        IMediator _mediator;

        public InventoryEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("Inventory")]
        public async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());
            var request = JsonConvert.DeserializeObject<PostInventoryRequestModel>(eventGridEvent.Data.ToString());
            var response = await _mediator.Send<PostInventoryResponseModel>(request);
            
        }

        [FunctionName("InventoryServiceBus")]
        public async Task InventoryServiceBus([ServiceBusTrigger("masstransitdemotopic", "masstransitdemosubscription", Connection = "ServiceBusConnectionString")]string message, ILogger log)
        {
            var request = JsonConvert.DeserializeObject<EventDetails>(message);
            var bookingDetails = JsonConvert.DeserializeObject<PostInventoryRequestModel>(JsonConvert.DeserializeObject<EventDetails>(request.Message.ToString()).Data.ToString());
            var response = await _mediator.Send<PostInventoryResponseModel>(bookingDetails);
        }
    }
}
