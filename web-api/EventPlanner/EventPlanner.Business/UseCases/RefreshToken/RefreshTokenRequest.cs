using MediatR;

namespace EventPlanner.Business.UseCases.RefreshToken;
public sealed class RefreshTokenRequest : IRequest<RefreshTokenResponse>
{
	public string? Email { get; set; }
	public string? OldRefreshToken { get; set; }
}
