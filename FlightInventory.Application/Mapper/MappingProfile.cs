using AutoMapper;
using FlightInventory.DataAccess.Entities;
using FlightInventory.Models.RequestModels;

namespace FlightInventory.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostInventoryRequestModel, Inventory>();
        }
    }
}
