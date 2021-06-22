using AutoMapper;
using FlightBooking.DataAccess.Repository;
using FlightBooking.Models.RequestModels;
using FlightBooking.Models.ResponseModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBooking.Application.QueryHandlers
{
    public class GetBookingDetailsQueryHandler : IRequestHandler<GetBookingDetailsRequestModel, GetBookingDetailsResponseModel>
    {
        private IBookingRepository _bookingRepo;
        private readonly IMapper _mapper;

        public GetBookingDetailsQueryHandler(IBookingRepository bookingRepo, IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _mapper = mapper;
        }

        public async Task<GetBookingDetailsResponseModel> Handle(GetBookingDetailsRequestModel request, CancellationToken cancellationToken)
        {
            var bookingDetails = String.IsNullOrWhiteSpace(request.Pnr) ? await _bookingRepo.GetBookingDetailsById(request.Id) : await _bookingRepo.GetBookingDetailsByPNR(request.Pnr);
            return _mapper.Map<GetBookingDetailsResponseModel>(bookingDetails);
        }
    }
}
