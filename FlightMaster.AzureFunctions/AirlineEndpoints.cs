using FlightBookingSystem.Common.Authentication;
using FlightBookingSystem.Common.Constant;
using FlightBookingSystem.Common.Enum;
using FlightBookingSystem.Common.FunctionAttributes;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FlightMaster.AzureFunctions
{
    public partial class FlightMasterEndpoints
    {
        [FunctionName("GetAirline")]
        [FunctionAuthentication]
        [CorrelationId]
        public async Task<IActionResult> GetAirline([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "airline/{id:int}")]
             HttpRequest req, int id)
        {
            var correlationId = _correlationInfo.GetCorrelationId(req);          
            int authorizationStatus = Convert.ToInt32(req.HttpContext.Request.Headers[CommonConstants.AuthorizationStatus]);
            if (authorizationStatus == (int)HttpStatusCode.Accepted)
            {
                _logger.LogMessages(correlationId, LoggingType.Info.ToString(), ResourceConstants.ValidRequest.Replace("{0}", "GetAirline"));
                GetAirlineRequestModel getAirlineRequest = new GetAirlineRequestModel { Id = id };
                var response = await _mediator.Send<GetAirlineResponseModel>(getAirlineRequest);
                if (response == null)
                    return new NotFoundResult();
                
                return new OkObjectResult(response);
            }
            else
            {
                _logger.LogMessages(correlationId, LoggingType.Error.ToString(), ResourceConstants.UnauthorizedRequest.Replace("{0}", "GetAirline"));
                return new UnauthorizedResult();
            }

        }
    }
}
