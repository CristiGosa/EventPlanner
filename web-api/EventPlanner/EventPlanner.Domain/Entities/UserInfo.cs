namespace EventPlanner.Domain.Entities;
public sealed class UserInfo
{
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public string? Email { get; set; }
	public string? LoginProvider { get; set; }
	public string? ProviderKey { get; set; }
}
