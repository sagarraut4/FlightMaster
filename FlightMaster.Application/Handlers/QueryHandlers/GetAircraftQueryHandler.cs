using AutoMapper;
using FlightMaster.DataAccess.Repository;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMaster.Application.Handlers.QueryHandlers
{
    public class GetAircraftQueryHandler : IRequestHandler<GetAircraftRequestModel, GetAircraftResponseModel>
    {
        private IAircraftRepository _aircraftRepo;
        private readonly IMapper _mapper;

        public GetAircraftQueryHandler(IAircraftRepository aircraftRepo, IMapper mapper)
        {
            _aircraftRepo = aircraftRepo;
            _mapper = mapper;
        }

        public async Task<GetAircraftResponseModel> Handle(GetAircraftRequestModel request, CancellationToken cancellationToken)
        {
            var airCraft = await _aircraftRepo.GetAircraftDetails(request.Id);
            return _mapper.Map<GetAircraftResponseModel>(airCraft);
        }
    }
}
