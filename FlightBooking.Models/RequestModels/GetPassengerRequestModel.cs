using FlightBooking.Models.ResponseModels;
using MediatR;

namespace FlightBooking.Models.RequestModels
{
    public class GetPassengerRequestModel : IRequest<GetPassengerResponseModel>
    {
        public int Id { get; set; }
    }
}
