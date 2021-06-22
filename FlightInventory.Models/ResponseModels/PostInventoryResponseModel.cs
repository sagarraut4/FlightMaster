using FlightInventory.Models.RequestModels;
using MediatR;

namespace FlightInventory.Models.ResponseModels
{
    public class PostInventoryResponseModel : IRequest<PostInventoryRequestModel>
    {
        public bool IsSuccess { get; set; }
    }
}
