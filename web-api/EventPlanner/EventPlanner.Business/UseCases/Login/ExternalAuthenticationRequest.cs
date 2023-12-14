using MediatR;

namespace EventPlanner.Business.UseCases.Login;
public sealed class ExternalAuthenticationRequest : IRequest<ExternalAuthenticationResponse>
{
	public string? Provider { get; set; }
	public string? IdToken { get; set; }
}
