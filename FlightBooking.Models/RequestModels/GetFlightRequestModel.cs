using MediatR;

namespace FlightBooking.Models.RequestModels
{
    public class GetFlightRequestModel : IRequest<GetFlightRequestModel>
    {
        public int Id { get; set; }
        public string FlightCode { get; set; }
    }
}
