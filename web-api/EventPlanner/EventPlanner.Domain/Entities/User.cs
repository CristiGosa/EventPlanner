using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Domain.Entities;

public sealed class User : IdentityUser
{
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public string? RefreshToken { get; set; }
	public DateTime? RefreshTokenExpire { get; set; }
}