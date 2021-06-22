using FlightBooking.Models.ResponseModels;
using MediatR;

namespace FlightBooking.Models.RequestModels
{
    public class GetBookingDetailsRequestModel : IRequest<GetBookingDetailsResponseModel>
    {
        public int Id { get; set; }
        public string Pnr { get; set; }
    }
}
