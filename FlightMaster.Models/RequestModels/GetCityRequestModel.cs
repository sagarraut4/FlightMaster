using FlightMaster.Models.ResponseModels;
using MediatR;

namespace FlightMaster.Models.RequestModels
{
    public class GetCityRequestModel : IRequest<GetCityResponseModel>
    {
        public int Id { get; set; }
    }
}
