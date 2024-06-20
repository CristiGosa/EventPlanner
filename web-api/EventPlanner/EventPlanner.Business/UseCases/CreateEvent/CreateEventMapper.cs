using AutoMapper;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.CreateEvent
{
    public sealed class CreateEventMapper : Profile
    {
        public CreateEventMapper() 
        {
            CreateMap<CreateEventRequest, Event>()
                .ForPath(dest => dest.LocationId, opt => opt.MapFrom(src => src.LocationId))
                .ForMember(dest => dest.OrganizerEmail, opt => opt.MapFrom(src => src.OrganizerEmail));

            CreateMap<Event, CreateEventResponse>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src));
        }
    }
}
