using AutoMapper;
using EventPlanner.Database.Models;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Database.Mappers
{
    internal class EventMapper : Profile
    {
        public EventMapper()
        {
            CreateMap<Event, EventData>().ReverseMap();
        }
    }
}
