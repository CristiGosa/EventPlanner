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

        public ICollection<int> EventsId { get; set; } = new List<int>();

        public float MapLatitude { get; set; }

        public float MapLongitude { get; set; }

        public string PlaceId { get; set; }
    }
}
