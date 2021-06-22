using AutoMapper;
using FlightMaster.DataAccess.Repository;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMaster.Application.Handlers.QueryHandlers
{
    public class GetCityQueryHandler : IRequestHandler<GetCityRequestModel, GetCityResponseModel>
    {
        private readonly ICityRepository _cityRepo;
        private readonly IMapper _mapper;

        public GetCityQueryHandler(ICityRepository cityRepo, IMapper mapper)
        {
            _cityRepo = cityRepo;
            _mapper = mapper;
        }

        public async Task<GetCityResponseModel> Handle(GetCityRequestModel request, CancellationToken cancellationToken)
        {
            var city = await _cityRepo.GetCityDetails(request.Id);
            return _mapper.Map<GetCityResponseModel>(city);
        }
    }
}
