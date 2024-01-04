using EventPlanner.Domain.Entities;
using EventPlanner.Database.Configuration;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Database.Models;

namespace EventPlanner.Database.Context;

public class DataContext : IdentityDbContext<User>
{
    public virtual DbSet<LocationData> Locations { get; set; }
	public virtual DbSet<EventData> Events { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EventData>()
			.HasOne(x => x.Location)
			.WithMany(x => x.Events)
			.HasForeignKey(x => x.LocationId);

		modelBuilder.Entity<EventData>()
			.HasOne(x => x.Organizer);

		modelBuilder.ApplyConfiguration(new RoleConfiguration());

		base.OnModelCreating(modelBuilder);
	}
}