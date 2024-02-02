using EventPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Database.Models
{
    [PrimaryKey(nameof(Id))]
    public class EventReservationData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User Attendee { get; set; }

        [Required]
        public int EventId { get; set; }

        public EventData Event { get; set; }
    }
}
