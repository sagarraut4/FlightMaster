using AutoMapper;
using FlightBooking.DataAccess.Entities;
using FlightBooking.DataAccess.Repository;
using FlightBooking.Models.RequestModels;
using FlightBooking.Models.ResponseModels;
using FlightBookingSystem.Common.Constant;
using FlightBookingSystem.Common.Helpers;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBooking.Application.QueryHandlers
{
    public class PostFlightBookingQueryHandler : IRequestHandler<PostFlightBookingRequestModel, PostFlightBookingResponseModel>
    {
        private IBookingRepository _bookingRepo;
        private readonly IMapper _mapper;
        private readonly IEventHelper _eventHelper;
        private readonly IPublishEndpoint _publishEndpoint;

        public PostFlightBookingQueryHandler(IBookingRepository bookingRepo, IMapper mapper, IEventHelper eventHelper, IPublishEndpoint publishEndpoint)
        {
            _bookingRepo = bookingRepo;
            _mapper = mapper;
            _eventHelper = eventHelper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<PostFlightBookingResponseModel> Handle(PostFlightBookingRequestModel request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request);
            var result = new PostFlightBookingResponseModel();
            result.Success = await _bookingRepo.BookFlight(booking);
            result.Success = true;
            // Publish update inventory event
            //await PublishInventoryUpdateEvent();
            await PublishUsingMassTransit();
            return result;
        }

        private async Task PublishUsingMassTransit()
        {
            var eventDetails = new EventDetails();
            eventDetails.EventType = eventDetails.Subject = EventConstants.InventoryUpdate;
            dynamic inventory = new ExpandoObject();
            inventory.FlightId = 1;
            inventory.EconomySeats = 2;
            inventory.PremiumEconomySeats = 2;
            inventory.BusinessSeats = 2;
            inventory.FirstClassSeats = 2;
            eventDetails.Data = inventory;

            await _publishEndpoint.Publish<EventDetails>(eventDetails);
        }

        private async Task PublishInventoryUpdateEvent()
        {
            var eventDetails = new EventDetails();
            eventDetails.EventType = eventDetails.Subject = EventConstants.InventoryUpdate;
            dynamic inventory =  new ExpandoObject();
            inventory.FlightId = 1;
            inventory.EconomySeats = 5;
            inventory.PremiumEconomySeats = 5;
            inventory.BusinessSeats = 5;
            inventory.FirstClassSeats = 5;
            eventDetails.Data = inventory;
            await _eventHelper.Publish(eventDetails);

            
        }
    }
}
