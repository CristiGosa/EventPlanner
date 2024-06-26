﻿using EventPlanner.Domain.Enum;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlanner.Database.Configuration;
internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
	public void Configure(EntityTypeBuilder<IdentityRole> builder)
	{
		builder.HasData(
		new IdentityRole
		{
			Id = Role.Organizer.ToString(),
			Name = Role.Organizer.ToString(),
			NormalizedName = "Organizer"
		},
		new IdentityRole
		{
			Id = Role.User.ToString(),
			Name = Role.User.ToString(),
			NormalizedName = "User"
		},
		new IdentityRole
		{
			Id = Role.Admin.ToString(),
			Name = Role.Admin.ToString(),
			NormalizedName = "Admin"
		}
		);
	}
}

