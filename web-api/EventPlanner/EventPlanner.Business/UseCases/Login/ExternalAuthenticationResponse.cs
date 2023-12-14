namespace EventPlanner.Business.UseCases.Login;
public sealed class ExternalAuthenticationResponse
{
	public string? Token { get; set; }
	public IList<string> Roles { get; set; }
	public string? RefreshToken { get; set; }
}
