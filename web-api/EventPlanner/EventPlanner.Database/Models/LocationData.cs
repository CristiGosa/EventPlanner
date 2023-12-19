using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Database.Models
{
    [PrimaryKey(nameof(Id))]
    [Index(nameof(Name), IsUnique = true)]
    public class LocationData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public int Capacity { get; set; }

        //public ICollection<DateTime> Bookings { get; set; } = new List<DateTime>(); to be mapped to events

        //public ICollection<EventData> Events { get; set; }
    }
}
