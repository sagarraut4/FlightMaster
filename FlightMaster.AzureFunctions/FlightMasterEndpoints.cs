using FlightBookingSystem.Common;

using FlightBookingSystem.Common.Logger;
using FlightBookingSystem.Common.Resource;
using MediatR;

namespace FlightMaster.AzureFunctions
{
    public partial class FlightMasterEndpoints
    {
        IMediator _mediator;
      
        ILogMessage _logger;
        ICorrelationInfo _correlationInfo;       

        public FlightMasterEndpoints(IMediator mediator, ILogMessage logger, 
                ICorrelationInfo correlationInfo)
        {
            _mediator = mediator;          
            _logger = logger;
            _correlationInfo = correlationInfo;           
        }
    }
}
