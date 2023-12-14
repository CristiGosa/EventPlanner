using Google.Apis.Auth;

using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EventPlanner.Authentication.Services
{
	public sealed class AuthenticationService : IAuthenticationService
	{
		private readonly IConfiguration _configuration;
		private readonly IConfigurationSection _jwtSettings;
		private readonly IConfigurationSection _googleSettings;
		private readonly IGoogleJsonWebSignature _googleJsonWebSignatureWrapper;
		private readonly JwtSecurityTokenHandler _jwtHandler;
		private const string Provider = "GOOGLE";

		public AuthenticationService(IConfiguration configuration, IGoogleJsonWebSignature googleJsonWebSignatureWrapper, JwtSecurityTokenHandler jwtHandler)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_jwtSettings = _configuration.GetSection("JwtSettings");
			_googleSettings = _configuration.GetSection("Authentication:Google");
			_googleJsonWebSignatureWrapper = googleJsonWebSignatureWrapper ?? throw new ArgumentNullException(nameof(googleJsonWebSignatureWrapper));
			_jwtHandler = jwtHandler ?? throw new ArgumentNullException(nameof(jwtHandler));
		}

		public async Task<UserInfo> VerifyGoogleTokenAsync(string idToken)
		{
			var settings = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string>() { _googleSettings.GetSection("clientId").Value }
			};

			var payload = await _googleJsonWebSignatureWrapper.ValidateAsync(idToken, settings);

			UserInfo userInfo = new UserInfo()
			{
				Name = payload.GivenName,
				Surname = payload.FamilyName,
				Email = payload.Email,
				LoginProvider = Provider,
				ProviderKey = payload.Subject
			};

			return userInfo;
		}

		public string GenerateToken(User user, IList<string> roles)
		{
			var signingCredentials = GetSigningCredentials();
			var claims = GetClaims(user, roles);
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			var token = _jwtHandler.WriteToken(tokenOptions);

			return token;
		}

		public RefreshToken GenerateRefreshToken()
		{
			var refreshToken = new RefreshToken
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				Expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings["refreshTokenExpirationInDays"])),
				Created = DateTime.Now
			};

			return refreshToken;
		}

		private SigningCredentials GetSigningCredentials()
		{
			var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);

			var secret = new SymmetricSecurityKey(key);

			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private List<Claim> GetClaims(IdentityUser user, IList<string> roles)
		{
			if (user == null)
			{
				return null;
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
		{
			var tokenOptions = new JwtSecurityToken(
				issuer: _jwtSettings["validIssuer"],
				audience: _jwtSettings["validAudience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
				signingCredentials: signingCredentials);

			return tokenOptions;
		}
	}
}
