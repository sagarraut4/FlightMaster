using FlightBooking.Models.ResponseModels;
using MediatR;

namespace FlightBooking.Models.RequestModels
{
    public class GetCustomerRequestModel : IRequest<GetCustomerResponseModel>
    {
        public int Id { get; set; }
    }
}
