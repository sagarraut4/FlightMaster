using FlightMaster.Models.ResponseModels;
using MediatR;

namespace FlightMaster.Models.RequestModels
{
    public class GetAirlineRequestModel : IRequest<GetAirlineResponseModel>
    {
        public int Id { get; set; }
    }
}
