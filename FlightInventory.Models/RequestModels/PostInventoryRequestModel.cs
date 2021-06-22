using FlightInventory.Models.ResponseModels;
using MediatR;

namespace FlightInventory.Models.RequestModels
{
    public class PostInventoryRequestModel : IRequest<PostInventoryResponseModel>
    {
        public int FlightId { get; set; }

        public int EconomySeats { get; set; }

        public int PremiumEconomySeats { get; set; }

        public int BusinessSeats { get; set; }

        public int FirstClassSeats { get; set; }
    }
}
