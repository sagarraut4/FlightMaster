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
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerRequestModel, GetCustomerResponseModel>
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;
        public GetCustomerQueryHandler(IRepository<Customer> customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<GetCustomerResponseModel> Handle(GetCustomerRequestModel request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepo.FindAsync(request.Id);
            return _mapper.Map<GetCustomerResponseModel>(customer);
        }
    }
}
