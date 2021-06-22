using FlightMaster.Models.ResponseModels;
using MediatR;
using System.Collections.Generic;

namespace FlightMaster.Models.RequestModels
{
    public class GetMultipleCitiesRequestModel : IRequest<IEnumerable<GetCityResponseModel>>
    {
        public string Ids { get; set; }
    }
}
