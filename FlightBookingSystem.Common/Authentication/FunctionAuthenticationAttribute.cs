using Microsoft.Azure.WebJobs.Host;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Authentication
{
    public class FunctionAuthenticationAttribute : FunctionInvocationFilterAttribute
    {
        private const string AUTH_HEADER_NAME = "Authorization";
        private const string BEARER_PREFIX = "Bearer";
        private readonly string _audience;
        private readonly string _issuer;
        private const string Space = " ";

        public FunctionAuthenticationAttribute()
        {
            _audience = Environment.GetEnvironmentVariable("Audience"); 
            _issuer = Environment.GetEnvironmentVariable("Issuer");
        }

        public override Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            var request = executingContext.Arguments.First().Value as Microsoft.AspNetCore.Http.HttpRequest;
            try
            {
                var authHeader = Convert.ToString(request.Headers[AUTH_HEADER_NAME]);

                // Get the token from the header
                if (!string.IsNullOrWhiteSpace(authHeader))
                {
                    var token = authHeader.Substring(string.Concat(BEARER_PREFIX, Space).Length);

                    //read issuer signing key here
                    var configManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{_issuer}/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
                    var openidconfig = configManager.GetConfigurationAsync().Result;

                    // Validate the token
                    var handler = new JwtSecurityTokenHandler();
                    var paserdJwt = handler.ReadJwtToken(token);
                    object kid;
                    paserdJwt.Header.TryGetValue("kid", out kid);
                    // Create the parameters
                    var tokenParams = new TokenValidationParameters()
                    {
                        RequireSignedTokens = true,
                        ValidAudience = _audience,
                        ValidateAudience = true,
                        ValidIssuer = _issuer,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = openidconfig.SigningKeys.Where(x => x.KeyId == Convert.ToString(kid)).FirstOrDefault()
                    };

                    var result = handler.ValidateToken(token, tokenParams, out var securityToken);
                    var currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                    var expDate = Convert.ToInt64(result.Claims.Where(c => c.Type == "exp").FirstOrDefault().Value);

                    if ((expDate - currentTimestamp) > 0)
                        request.Headers.Add("AuthorizationStatus", Convert.ToInt32(HttpStatusCode.Accepted).ToString());
                    else
                        request.Headers.Add("AuthorizationStatus", Convert.ToInt32(HttpStatusCode.Unauthorized).ToString());

                }
                else
                {
                    request.Headers.Add("AuthorizationStatus", Convert.ToInt32(HttpStatusCode.Unauthorized).ToString());
                }

            }
            catch (SecurityTokenExpiredException e)
            {
                request.Headers.Add("AuthorizationStatus", Convert.ToInt32(HttpStatusCode.Unauthorized).ToString());
            }
            catch (System.Exception ex)
            {
                request.Headers.Add("AuthorizationStatus", Convert.ToInt32(HttpStatusCode.InternalServerError).ToString());
            }
            return base.OnExecutingAsync(executingContext, cancellationToken);
        }
    }
}
