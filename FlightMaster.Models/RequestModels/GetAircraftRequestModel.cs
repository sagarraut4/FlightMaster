using FlightMaster.Models.ResponseModels;
using MediatR;

namespace FlightMaster.Models.RequestModels
{
    public class GetAircraftRequestModel : IRequest<GetAircraftResponseModel>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public GetAirlineResponseModel Airline { get; set; }
        public GetCarrierTypeResponseModel CarrierType { get; set; }
    }
}
