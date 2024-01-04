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
        public Location Location { get; set; }
    }
}
