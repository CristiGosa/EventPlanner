using Google.Apis.Auth;

namespace EventPlanner.Authentication.Services;
public class GoogleJsonWebSignatureWrapper : IGoogleJsonWebSignature
{
	public async Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken, GoogleJsonWebSignature.ValidationSettings settings)
	{
		return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
	}
}
