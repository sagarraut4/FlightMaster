using FlightBooking.Models.ResponseModels;
using MediatR;

namespace FlightBooking.Models.RequestModels
{
    public class PostFlightBookingRequestModel : IRequest<PostFlightBookingResponseModel>
    {
        public int FlightId { get; set; }
        public int CustomerId { get; set; }
        public int Seats { get; set; }
        public string SeatsType { get; set; }
        public decimal Fare { get; set; }
        public string Status { get; set; }
        public string PNR { get; set; }
        public int PassengerId { get; set; }
    }
}
