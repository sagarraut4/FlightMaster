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
using Microsoft.Extensions.Logging;
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
        [FunctionName("GetCity")]
        [FunctionAuthentication]
        [CorrelationId]
        public async Task<IActionResult> GetCity([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "city/{id:int}")]
 HttpRequest req, int id, ILogger log)
        {
            var correlationId = _correlationInfo.GetCorrelationId(req);

            int authorizationStatus = Convert.ToInt32(req.HttpContext.Request.Headers[CommonConstants.AuthorizationStatus]);
            if (authorizationStatus == (int)HttpStatusCode.Accepted)
            {
                _logger.LogMessages(correlationId, LoggingType.Info.ToString(), ResourceConstants.ValidRequest.Replace("{0}", "GetCity"));
                GetCityRequestModel getCityRequest = new GetCityRequestModel { Id = id };
                var response = await _mediator.Send<GetCityResponseModel>(getCityRequest);
                if (response == null)
                    return new NotFoundResult();

                return new OkObjectResult(response);
            }
            else
            {
                _logger.LogMessages(correlationId, LoggingType.Error.ToString(), ResourceConstants.UnauthorizedRequest.Replace("{0}", "GetCity"));
                return new UnauthorizedResult();
            }
        }

        [FunctionName("GetMultipleCities")]
        public async Task<IActionResult> GetMultipleCities([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "city")]
 HttpRequest req, ILogger log)
        {
            var correlationId = _correlationInfo.GetCorrelationId(req);
            req.HttpContext.Response.Headers.Add(CommonConstants.CorrelationIdHeader, correlationId);

            string authorizationStatus = req.HttpContext.Request.Headers[CommonConstants.AuthorizationStatus].ToString();
            if (Convert.ToInt32(authorizationStatus).Equals((int)HttpStatusCode.Accepted))
            {
                _logger.LogMessages(correlationId, LoggingType.Info.ToString(), ResourceConstants.ValidRequest.Replace("{0}", "GetMultipleCities"));
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<GetMultipleCitiesRequestModel>(requestBody);

                var response = await _mediator.Send<IEnumerable<GetCityResponseModel>>(request);
                if (response == null)
                    return new NotFoundResult();

                return new OkObjectResult(response);
            }
            else
            {
                _logger.LogMessages(correlationId, LoggingType.Error.ToString(), ResourceConstants.UnauthorizedRequest.Replace("{0}", "GetMultipleCities"));
                return new UnauthorizedResult();
            }
        }
    }
}
