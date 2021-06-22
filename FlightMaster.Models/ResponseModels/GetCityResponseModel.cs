using System;

namespace FlightMaster.Models.ResponseModels
{
    public class GetCityResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public int CountryId { get; set; }

        public DateTimeOffset? TimeZone { get; set; }

        public GetCountryResponseModel Country { get; set; }
    }
}
