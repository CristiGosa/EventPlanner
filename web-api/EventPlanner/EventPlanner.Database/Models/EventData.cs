using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Database.Models
{
    [PrimaryKey(nameof(Id))]
    public class EventData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public float TicketPrice { get; set; }

        public Currency PriceCurrency { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public User Organizer { get; set; }

        public int LocationId { get; set; }

        public EventStatus Status { get; set; }

        public int ParticipantsNumber { get; set; }

        public string? PhotoUrl { get; set; }

        public ICollection<int> ReservationsId { get; set; } = new List<int>();
    }
}
