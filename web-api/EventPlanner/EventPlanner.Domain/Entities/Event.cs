using EventPlanner.Domain.Enum;

namespace EventPlanner.Domain.Entities
{
    public sealed class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float TicketPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrganizerEmail { get; set; }
        public int LocationId { get; set; }
        public EventStatus Status { get; set; }
        public int ParticipantsNumber { get; set; } = 0;
        public ICollection<int> ReservationsId { get; set; } = new List<int>();
    }
}
