using Google.Apis.Auth;

namespace EventPlanner.Authentication.Services;
public interface IGoogleJsonWebSignature
{
	Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken, GoogleJsonWebSignature.ValidationSettings settings);
}