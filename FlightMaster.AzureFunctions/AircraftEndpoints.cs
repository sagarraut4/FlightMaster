using FlightBookingSystem.Common.Authentication;
using FlightBookingSystem.Common.Constant;
using FlightBookingSystem.Common.Enum;
using FlightBookingSystem.Common.FunctionAttributes;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FlightMaster.AzureFunctions
{

    public partial class FlightMasterEndpoints
    {
        [FunctionName("GetAircraft")]
        [FunctionAuthentication]
        [CorrelationId]
        public async Task<IActionResult> GetAircraft([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "aircraft/{id:int}")]
            HttpRequest req, int id)
        {
            var correlationId = _correlationInfo.GetCorrelationId(req);
           
            int authorizationStatus = Convert.ToInt32(req.HttpContext.Request.Headers[CommonConstants.AuthorizationStatus]);
            if (authorizationStatus == (int)HttpStatusCode.Accepted)
            {               
                _logger.LogMessages(correlationId, LoggingType.Info.ToString(), ResourceConstants.ValidRequest.Replace("{0}", "GetAircraft"));
                GetAircraftRequestModel getAircraftRequest = new GetAircraftRequestModel { Id = id };
                var response = await _mediator.Send<GetAircraftResponseModel>(getAircraftRequest);
                if (response == null)
                    return new NotFoundResult();

                return new OkObjectResult(response);
            }
            else
            {

                _logger.LogMessages(correlationId, LoggingType.Error.ToString(), ResourceConstants.UnauthorizedRequest.Replace("{0}", "GetAircraft"));
                return new UnauthorizedResult();
            }
        }

        [FunctionName("GetMultipleAircraft")]
        public async Task<IActionResult> GetMultipleAircraft([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "aircraft")]
            HttpRequest req)
        {
            var correlationId = _correlationInfo.GetCorrelationId(req);
            req.HttpContext.Response.Headers.Add(CommonConstants.CorrelationIdHeader, correlationId);

            string authorizationStatus = req.HttpContext.Request.Headers[CommonConstants.AuthorizationStatus].ToString();
            if (Convert.ToInt32(authorizationStatus).Equals((int)HttpStatusCode.Accepted))
            {
                _logger.LogMessages(correlationId, LoggingType.Info.ToString(), ResourceConstants.ValidRequest.Replace("{0}", "GetMultipleAircraft"));
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<GetMultipleAircraftRequestModel>(requestBody);

                var response = await _mediator.Send<IEnumerable<GetAircraftResponseModel>>(request);
                if (response == null)
                    return new NotFoundResult();

                return new OkObjectResult(response);
            }
            else
            {
                _logger.LogMessages(correlationId, LoggingType.Error.ToString(), ResourceConstants.UnauthorizedRequest.Replace("{0}", "GetMultipleAircraft"));
                return new UnauthorizedResult();
            }
        }
    }
}
