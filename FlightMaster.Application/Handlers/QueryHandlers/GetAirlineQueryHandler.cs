using AutoMapper;
using FlightBookingSystem.Common.Cache;
using FlightBookingSystem.Common.Constant;
using FlightMaster.DataAccess.Entities;
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
    public class GetAirlineQueryHandler : IRequestHandler<GetAirlineRequestModel, GetAirlineResponseModel>
    {
        private readonly IRepository<Airline> _airlineRepo;
        private readonly IMapper _mapper;
        private readonly ICacheRepository _cacheRepo;
        public GetAirlineQueryHandler(IRepository<Airline> airlineRepo, IMapper mapper, ICacheRepository cacheRepo)
        {
            _airlineRepo = airlineRepo;
            _mapper = mapper;
            _cacheRepo = cacheRepo;
        }

        public async Task<GetAirlineResponseModel> Handle(GetAirlineRequestModel request, CancellationToken cancellationToken)
        {
            var airlinesCache = await _cacheRepo.Get<IEnumerable<Airline>>(CacheConstants.AirlineKey);
            if (airlinesCache == null)
            {
                airlinesCache = await _airlineRepo.ToListAsync();
                await _cacheRepo.Add(CacheConstants.AirlineKey, airlinesCache);
            }

            var airline = airlinesCache.SingleOrDefault(a => a.Id == request.Id);
            return _mapper.Map<GetAirlineResponseModel>(airline);
        }
    }
}
