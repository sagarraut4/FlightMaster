using AutoMapper;
using FlightMaster.DataAccess.Entities;
using FlightMaster.Models.ResponseModels;
using System.Collections.Generic;

namespace FlightMaster.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarrierType, GetCarrierTypeResponseModel>();
            CreateMap<Airline, GetAirlineResponseModel>();
            CreateMap<Aircraft, GetAircraftResponseModel>();
            CreateMap<IEnumerable<Aircraft>, List<GetAircraftResponseModel>>();
            CreateMap<Country, GetCountryResponseModel>();
            CreateMap<City, GetCityResponseModel>();
            CreateMap<IEnumerable<City>, List<GetCityResponseModel>>();
        }
    }
}
