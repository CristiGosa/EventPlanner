using AutoMapper;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.CreateEventReservation
{
    public sealed class CreateEventReservationMapper : Profile
    {
        public CreateEventReservationMapper()
        {
            CreateMap<CreateEventReservationRequest, EventReservation>()
                .ForPath(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.AttendeeEmail, opt => opt.MapFrom(src => src.AttendeeEmail));
        }
    }
}
