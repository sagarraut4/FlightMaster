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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace FlightMaster.Test
{
    [TestClass]
    public class CarrierTypeTests : TestCore
    {
        [TestMethod]
        public async Task Get_Carrier_Type_Test()
        {
            var query = new Dictionary<string, StringValues>();
            var body = "";
            var mediator = new Mock<IMediator>();

            var carrierTypeMock = new GetCarrierTypeResponseModel { Id = 1, Type = "Carrier Type 1", BusinessSeats = 50, EconomySeats = 100, FirstClassSeats = 30, PremiumEconomySeats = 70, RangeNMI = "Range NMI" };
            mediator.Setup(x => x.Send(It.IsAny<GetCarrierTypeRequestModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(carrierTypeMock);
                    
            var logger = new Mock<ILogMessage>();
            var correlationInfo = new Mock<ICorrelationInfo>();
           
            var flightMasterEndpoints = new FlightMasterEndpoints(mediator.Object, logger.Object, correlationInfo.Object);

            var result = await flightMasterEndpoints.GetCarrierType(req: HttpRequestSetup(query, body), carrierTypeMock.Id, log: null);

            var resultObject = (OkObjectResult)result;
            mediator.Verify(x => x.Send(It.Is<GetCarrierTypeRequestModel>(y => y.Id == carrierTypeMock.Id), It.IsAny<CancellationToken>()));
            var response = resultObject.Value as GetCarrierTypeResponseModel;

            Assert.AreEqual(carrierTypeMock.Id, response.Id);
            Assert.AreEqual(carrierTypeMock.Type, response.Type);
            Assert.AreEqual(carrierTypeMock.BusinessSeats, response.BusinessSeats);
            Assert.AreEqual(carrierTypeMock.EconomySeats, response.EconomySeats);
            Assert.AreEqual(carrierTypeMock.FirstClassSeats, response.FirstClassSeats);
            Assert.AreEqual(carrierTypeMock.PremiumEconomySeats, response.PremiumEconomySeats);
            Assert.AreEqual(carrierTypeMock.RangeNMI, response.RangeNMI);
        }
    }
}
