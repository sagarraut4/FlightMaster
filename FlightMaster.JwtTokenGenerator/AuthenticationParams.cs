namespace FlightMaster.JwtTokenGenerator
{
    public class AuthenticationParams
    {
        public string AuthUrl { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
