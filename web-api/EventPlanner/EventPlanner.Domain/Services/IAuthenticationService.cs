using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Services;
public interface IAuthenticationService
{
	Task<UserInfo> VerifyGoogleTokenAsync(string idToken);
	string GenerateToken(User user, IList<string> roles);
	RefreshToken GenerateRefreshToken();
}
