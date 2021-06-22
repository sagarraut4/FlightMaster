using AutoMapper;
using FlightMaster.DataAccess.Repository;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMaster.Application.Handlers
{
    public class GetMultipleCitiesQueryHandler : IRequestHandler<GetMultipleCitiesRequestModel, IEnumerable<GetCityResponseModel>>
    {
        private readonly ICityRepository _cityRepo;
        private readonly IMapper _mapper;

        public GetMultipleCitiesQueryHandler(ICityRepository cityRepo, IMapper mapper)
        {
            _cityRepo = cityRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCityResponseModel>> Handle(GetMultipleCitiesRequestModel request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepo.GetMultipleCitiesDetails(request.Ids.Split(',').Select(int.Parse));
            return _mapper.Map<IEnumerable<GetCityResponseModel>>(cities);
        }
    }
}
