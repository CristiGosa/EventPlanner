using EventPlanner.Domain.Entities;
using EventPlanner.Database.Configuration;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Database.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace EventPlanner.Database.Context;

public class DataContext : IdentityDbContext<User>
{
    public virtual DbSet<LocationData> Locations { get; set; }
	public virtual DbSet<EventData> Events { get; set; }
	public virtual DbSet<EventReservationData> EventReservations { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EventData>()
			.Property(x => x.ReservationsId)
			.HasConversion(new ValueConverter<ICollection<int>, string>(
			v => JsonConvert.SerializeObject(v),
			v => JsonConvert.DeserializeObject<ICollection<int>>(v)));

        modelBuilder.Entity<EventData>()
			.HasOne(x => x.Organizer);

		modelBuilder.Entity<LocationData>()
			.Property(x => x.EventsId)
            .HasConversion(new ValueConverter<ICollection<int>, string>(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<ICollection<int>>(v)));

        modelBuilder.Entity<EventReservationData>()
            .HasOne(x => x.Attendee);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());

		base.OnModelCreating(modelBuilder);
	}
}