using AutoMapper;
using EventPlanner.Database.Models;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Database.Mappers
{
    internal class EventReservationMapper : Profile
    {
        public EventReservationMapper()
        {
            CreateMap<EventReservation, EventReservationData>().ReverseMap();
        }
    }
}
