using FlightMaster.Models.ResponseModels;
using MediatR;
using System.Collections.Generic;

namespace FlightMaster.Models.RequestModels
{
    public class GetMultipleAircraftRequestModel : IRequest<IEnumerable<GetAircraftResponseModel>>
    {
        public string Ids { get; set; }
    }
}
