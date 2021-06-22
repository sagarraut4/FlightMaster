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
    public class CityTests : TestCore
    {
        [TestMethod]
        public async Task Get_City_Test()
        {
            var query = new Dictionary<string, StringValues>();
            var body = "";
            var mediator = new Mock<IMediator>();

            var countryMock = new GetCountryResponseModel { Name = "Country", Code = "Country 101", Id = 1 };
            var cityMock = new GetCityResponseModel { Id = 1, Code = "City 101", Name = "City", Country = countryMock, CountryId = 1, TimeZone = DateTimeOffset.Now };
            mediator.Setup(x => x.Send(It.IsAny<GetCityRequestModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(cityMock);

           
            var logger = new Mock<ILogMessage>();
            var correlationInfo = new Mock<ICorrelationInfo>();
          
            var sut = new FlightMasterEndpoints(mediator.Object, logger.Object, correlationInfo.Object);
           
            var result = await sut.GetCity(req: HttpRequestSetup(query, body), cityMock.Id, log: null);

            var resultObject = (OkObjectResult)result;
            mediator.Verify(x => x.Send(It.Is<GetCityRequestModel>(y => y.Id == cityMock.Id), It.IsAny<CancellationToken>()));
            var res = resultObject.Value as GetCityResponseModel;

            Assert.AreEqual(cityMock.Id, res.Id);
            Assert.AreEqual(cityMock.Code, res.Code);
            Assert.AreEqual(cityMock.Name, res.Name);
            Assert.AreEqual(cityMock.CountryId, res.CountryId);
            Assert.AreEqual(cityMock.TimeZone, res.TimeZone);

            Assert.AreEqual(cityMock.Country.Id, res.Country.Id);
            Assert.AreEqual(cityMock.Country.Name, res.Country.Name);
            Assert.AreEqual(cityMock.Country.Code, res.Country.Code);
        }

        [TestMethod]
        public async Task Get_Multiple_City_Test()
        {
            var query = new Dictionary<string, StringValues>();
            var body = "";
            var mediator = new Mock<IMediator>();

            var countryMock = new GetCountryResponseModel { Name = "Country", Code = "Country 101", Id = 1 };
            var cityMock = new GetCityResponseModel { Id = 1, Code = "City 101", Name = "City", Country = countryMock, CountryId = 1, TimeZone = DateTimeOffset.Now };
            List<GetCityResponseModel> list = new List<GetCityResponseModel>
            {
                cityMock
            };
            mediator.Setup(x => x.Send(It.IsAny<GetMultipleCitiesRequestModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(list);

          
            var logger = new Mock<ILogMessage>();
            var correlationInfo = new Mock<ICorrelationInfo>();
           
            var flightMasterEndpoints = new FlightMasterEndpoints(mediator.Object, logger.Object, correlationInfo.Object);
            
            var result = await flightMasterEndpoints.GetMultipleCities(req: HttpRequestSetup(query, body), log: null);

            var resultObject = (OkObjectResult)result;
            var response = resultObject.Value as IEnumerable<GetCityResponseModel>;
            var cityResult = response.FirstOrDefault();
            Assert.AreEqual(cityMock.Id, cityResult.Id);
            Assert.AreEqual(cityMock.Code, cityResult.Code);
            Assert.AreEqual(cityMock.Name, cityResult.Name);
            Assert.AreEqual(cityMock.Country, cityResult.Country);
        }
    }
}
