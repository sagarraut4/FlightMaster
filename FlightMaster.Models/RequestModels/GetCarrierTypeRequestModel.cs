using FlightMaster.Models.ResponseModels;
using MediatR;

namespace FlightMaster.Models.RequestModels
{
    public class GetCarrierTypeRequestModel : IRequest<GetCarrierTypeResponseModel>
    {
        public int Id { get; set; }
    }
}
