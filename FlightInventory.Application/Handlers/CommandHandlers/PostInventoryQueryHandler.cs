using AutoMapper;
using FlightInventory.DataAccess.Entities;
using FlightInventory.DataAccess.Repository.Interfaces;
using FlightInventory.Models.RequestModels;
using FlightInventory.Models.ResponseModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlightInventory.Application.Handlers.CommandHandlers
{
    public class PostInventoryQueryHandler : IRequestHandler<PostInventoryRequestModel, PostInventoryResponseModel>
    {
        private IRepository<Inventory> _inventoryRepo;
        private readonly IMapper _mapper;

        public PostInventoryQueryHandler(IRepository<Inventory> inventoryRepo, IMapper mapper)
        {
            _inventoryRepo = inventoryRepo;
            _mapper = mapper;
        }

        public async Task<PostInventoryResponseModel> Handle(PostInventoryRequestModel request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepo.FindAsync(i => i.FlightId == request.FlightId);
            inventory.EconomySeats = inventory.EconomySeats - request.EconomySeats;
            inventory.BusinessSeats = inventory.BusinessSeats - request.BusinessSeats;
            inventory.PremiumEconomySeats = inventory.PremiumEconomySeats - request.PremiumEconomySeats;
            inventory.FirstClassSeats = inventory.FirstClassSeats - request.FirstClassSeats;
            await _inventoryRepo.UpdateAsync(inventory);
            var result = new PostInventoryResponseModel();
            result.IsSuccess = true;
            return result;
        }
    }
}
