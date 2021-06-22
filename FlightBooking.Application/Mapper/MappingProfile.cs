using AutoMapper;
using FlightBooking.DataAccess.Entities;
using FlightBooking.Models.RequestModels;
using FlightBooking.Models.ResponseModels;

namespace FlightBooking.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, GetBookingDetailsResponseModel>();
            CreateMap<Customer, GetCustomerResponseModel>();
            CreateMap<Passenger, GetPassengerResponseModel>();
            CreateMap<Booking, PostFlightBookingRequestModel>();
            CreateMap<PostFlightBookingRequestModel, Booking>();
        }
    }
}
