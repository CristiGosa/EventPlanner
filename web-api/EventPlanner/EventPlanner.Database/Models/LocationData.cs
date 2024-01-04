﻿using Microsoft.EntityFrameworkCore;
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

        public ICollection<EventData>? Events { get; set; } = new List<EventData>();
    }
}