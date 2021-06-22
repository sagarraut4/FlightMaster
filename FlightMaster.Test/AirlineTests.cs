using FlightBookingSystem.Common;
using FlightBookingSystem.Common.Logger;
using FlightBookingSystem.Common.Resource;
using FlightMaster.AzureFunctions;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMaster.Test
{
    [TestClass]
    public class AirlineTests : TestCore
    {
        [TestMethod]
        public async Task Get_Airline_Test()
        {
            var query = new Dictionary<string, StringValues>();
            var body = "";
            var mediator = new Mock<IMediator>();

            var airlineMock = new GetAirlineResponseModel { Id = 1, Name = "AL-101", Address1 = "Address 1", Address2 = "Address 2", Address3 = "Address 3" };
            mediator.Setup(x => x.Send(It.IsAny<GetAirlineRequestModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(airlineMock);

          
            var logger = new Mock<ILogMessage>();
            var correlationInfo = new Mock<ICorrelationInfo>();           
           
            var flightMasterEndpoints = new FlightMasterEndpoints(mediator.Object, logger.Object, correlationInfo.Object);
            var result = await flightMasterEndpoints.GetAirline(req: HttpRequestSetup(query, body), airlineMock.Id);

            var resultObject = (OkObjectResult)result;
            mediator.Verify(x => x.Send(It.Is<GetAirlineRequestModel>(y => y.Id == airlineMock.Id), It.IsAny<CancellationToken>()));
            var response = resultObject.Value as GetAirlineResponseModel;

            Assert.AreEqual(airlineMock.Id, response.Id);
            Assert.AreEqual(airlineMock.Name, response.Name);
            Assert.AreEqual(airlineMock.Address1, response.Address1);
            Assert.AreEqual(airlineMock.Address2, response.Address2);
            Assert.AreEqual(airlineMock.Address3, response.Address3);
        }
    }
}
