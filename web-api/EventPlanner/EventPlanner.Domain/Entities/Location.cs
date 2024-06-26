﻿namespace EventPlanner.Domain.Entities
{
    public sealed class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<int> EventsId { get; set; } = new List<int>();
        public float MapLatitude { get; set; }
        public float MapLongitude { get; set; }
        public string PlaceId { get; set; }
    }
}
