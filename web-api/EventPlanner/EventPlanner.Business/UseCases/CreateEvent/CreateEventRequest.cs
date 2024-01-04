using EventPlanner.Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace EventPlanner.Business.UseCases.CreateEvent
{
    public sealed record CreateEventRequest : IRequest<Event>
    {
        public int LocationId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public float TicketPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public string? OrganizerEmail { get; set; }
    }
}
