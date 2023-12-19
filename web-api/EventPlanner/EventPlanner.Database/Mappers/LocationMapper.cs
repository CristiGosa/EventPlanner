using AutoMapper;
using EventPlanner.Database.Models;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Database.Mappers
{
    internal class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<Location, LocationData>().ReverseMap();
        }
    }
}
