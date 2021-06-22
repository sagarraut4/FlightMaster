using AutoMapper;
using FlightBooking.DataAccess.Entities;
using FlightBooking.DataAccess.Repository;
using FlightBooking.Models.RequestModels;
using FlightBooking.Models.ResponseModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBooking.Application.QueryHandlers
{
    public class GetPassengerQueryHandler : IRequestHandler<GetPassengerRequestModel, GetPassengerResponseModel>
    {
        private readonly IRepository<Passenger> _customerRepo;
        private readonly IMapper _mapper;
        public GetPassengerQueryHandler(IRepository<Passenger> customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<GetPassengerResponseModel> Handle(GetPassengerRequestModel request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepo.FindAsync(request.Id);
            return _mapper.Map<GetPassengerResponseModel>(customer);
        }
    }
}
