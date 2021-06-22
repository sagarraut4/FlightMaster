using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;

namespace FlightMaster.JwtTokenGenerator
{
    public static class JwtTokenGenerator
    {
        [FunctionName("JwtTokenGenerator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            [RequestBodyType(typeof(AuthenticationParams), "token request")]HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var parsedRequest = JObject.Parse(requestBody);
            AuthenticationParams authParams = parsedRequest.ToObject<AuthenticationParams>();
            AuthenticationContext ac = new AuthenticationContext(authParams.AuthUrl);

            var token = ac.AcquireTokenAsync(authParams.Audience, new ClientCredential(authParams.ClientId, authParams.ClientSecret)).GetAwaiter().GetResult();

            return new OkObjectResult(token?.AccessToken);
        }
    }
}
