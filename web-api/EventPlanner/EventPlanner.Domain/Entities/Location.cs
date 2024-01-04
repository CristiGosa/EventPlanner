namespace EventPlanner.Domain.Entities
{
    public sealed class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<Event>? Events { get; set; } = new List<Event>();
    }
}
