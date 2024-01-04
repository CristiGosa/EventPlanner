using EventPlanner.Domain.Entities;
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

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public User Organizer { get; set; }

        public int LocationId { get; set; }

        public LocationData Location { get; set; }
    }
}
