using FlightBookingSystem.Common;
using FlightBookingSystem.Common.Logger;
using FlightBookingSystem.Common.Resource;
using FlightMaster.AzureFunctions;
using FlightMaster.Models.RequestModels;
using FlightMaster.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMaster.Test
{

    [TestClass]
    public class AircraftTests : TestCore
    {
        [TestMethod]
        public async Task Get_Aircraft_Test()
        {
            var query = new Dictionary<string, StringValues>();
            var body = "";
            var mediator = new Mock<IMediator>();

            var aircraftMock = new GetAircraftResponseModel { Id = 1, Code = "AC-101", AirlineId = 1001, CarrierId = 5001 };
            mediator.Setup(x => x.Send(It.IsAny<GetAircraftRequestModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(aircraftMock);
            
            var logger = new Mock<ILogMessage>();
            var correlationInfo = new Mock<ICorrelationInfo>();
           
            var flightMasterEndpoints = new FlightMasterEndpoints(mediator.Object, logger.Object, correlationInfo.Object);
            var result = await flightMasterEndpoints.GetAircraft(req: HttpRequestSetup(query, body), aircraftMock.Id);

            var resultObject = (OkObjectResult)result;
            mediator.Verify(x => x.Send(It.Is<GetAircraftRequestModel>(y => y.Id == aircraftMock.Id), It.IsAny<CancellationToken>()));
            var resposne = resultObject.Value as GetAircraftResponseModel;

            Assert.AreEqual(aircraftMock.Id, resposne.Id);
            Assert.AreEqual(aircraftMock.Code, resposne.Code);
            Assert.AreEqual(aircraftMock.AirlineId, resposne.AirlineId);
            Assert.AreEqual(aircraftMock.CarrierId, resposne.CarrierId);
        }

        [TestMethod]
        public async Task Get_Multiple_Aircraft_Test()
        {
            var query = new Dictionary<string, StringValues>();
            var body = "";
            var mediator = new Mock<IMediator>();

            var aircraftMock = new GetAircraftResponseModel { Id = 1, Code = "AC-101", AirlineId = 1001, CarrierId = 5001 };
            List<GetAircraftResponseModel> list = new List<GetAircraftResponseModel>
            {
                aircraftMock
            };

            var countryMock = new GetCountryResponseModel { Name = "Country", Code = "Country 101", Id = 1 };
            var cityMock = new GetCityResponseModel { Id = 1, Code = "City 101", Name = "City", Country = countryMock, CountryId = 1, TimeZone = DateTimeOffset.Now };

            mediator.Setup(x => x.Send(It.IsAny<GetMultipleAircraftRequestModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(list);

            var sut = new FlightMasterEndpoints(mediator.Object,null,null);
            var result = await sut.GetMultipleAircraft(req: HttpRequestSetup(query, body));

            var resultObject = (OkObjectResult)result;
            var res = resultObject.Value as IEnumerable<GetAircraftResponseModel>;
            var aircraftResult = res.FirstOrDefault();
            Assert.AreEqual(aircraftMock.Id, aircraftResult.Id);
            Assert.AreEqual(aircraftMock.Code, aircraftResult.Code);
            Assert.AreEqual(aircraftMock.AirlineId, aircraftResult.AirlineId);
            Assert.AreEqual(aircraftMock.CarrierId, aircraftResult.CarrierId);
        }
    }
}
