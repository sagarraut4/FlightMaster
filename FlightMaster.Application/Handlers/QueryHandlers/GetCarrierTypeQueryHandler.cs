using AutoMapper;
using FlightMaster.DataAccess.Entities;
using FlightMaster.DataAccess.Repository;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMaster.Application.Handlers.QueryHandlers
{
    public class GetCarrierTypeQueryHandler : IRequestHandler<GetCarrierTypeRequestModel, GetCarrierTypeResponseModel>
    {
        private readonly IRepository<CarrierType> _carrierTypeRepo;
        private readonly IMapper _mapper;
        public GetCarrierTypeQueryHandler(IRepository<CarrierType> carrierRepo, IMapper mapper)
        {
            _carrierTypeRepo = carrierRepo;
            _mapper = mapper;
        }

        public async Task<GetCarrierTypeResponseModel> Handle(GetCarrierTypeRequestModel request, CancellationToken cancellationToken)
        {
            var carrierType = await _carrierTypeRepo.FindAsync(request.Id);
            return _mapper.Map<GetCarrierTypeResponseModel>(carrierType);
        }
    }
}
