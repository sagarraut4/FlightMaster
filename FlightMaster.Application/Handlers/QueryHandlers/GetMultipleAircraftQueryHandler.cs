using AutoMapper;
using FlightMaster.DataAccess.Repository;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMaster.Application.Handlers.QueryHandlers
{
    public class GetMultipleAircraftQueryHandler : IRequestHandler<GetMultipleAircraftRequestModel, IEnumerable<GetAircraftResponseModel>>
    {
        private IAircraftRepository _aircraftRepo;
        private readonly IMapper _mapper;

        public GetMultipleAircraftQueryHandler(IAircraftRepository aircraftRepo, IMapper mapper)
        {
            _aircraftRepo = aircraftRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAircraftResponseModel>> Handle(GetMultipleAircraftRequestModel request, CancellationToken cancellationToken)
        {
            var aircrafts = await _aircraftRepo.GetMultipleAircraftDetails(request.Ids.Split(',').Select(int.Parse));
            return _mapper.Map<IEnumerable<GetAircraftResponseModel>>(aircrafts);
        }
    }
}
