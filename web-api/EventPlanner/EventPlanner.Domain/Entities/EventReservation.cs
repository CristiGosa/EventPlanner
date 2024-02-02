﻿namespace EventPlanner.Domain.Entities
{
    public sealed class EventReservation
    {
        public int Id { get; set; }
        public string AttendeeEmail { get; set; }
        public int EventId { get; set; }
    }
}
