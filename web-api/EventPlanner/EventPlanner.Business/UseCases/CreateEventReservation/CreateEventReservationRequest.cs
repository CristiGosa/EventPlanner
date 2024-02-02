using EventPlanner.Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace EventPlanner.Business.UseCases.CreateEventReservation
{
    public sealed record CreateEventReservationRequest : IRequest<EventReservation>
    {
        public int EventId { get; set; }

        [JsonIgnore]
        public string? AttendeeEmail { get; set; }
    }
}
