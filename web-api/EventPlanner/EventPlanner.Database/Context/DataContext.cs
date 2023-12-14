using EventPlanner.Domain.Entities;
using EventPlanner.Database.Configuration;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Database.Context;

public class DataContext : IdentityDbContext<User>
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new RoleConfiguration());

		base.OnModelCreating(modelBuilder);
	}
}