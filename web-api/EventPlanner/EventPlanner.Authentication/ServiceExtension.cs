using EventPlanner.Authentication.Services;
using EventPlanner.Domain.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EventPlanner.Authentication;
public static class ServiceExtension
{
	public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection("JwtSettings");

		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings["validIssuer"],
				ValidAudience = jwtSettings["validAudience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
					.GetBytes(jwtSettings.GetSection("securityKey").Value))
			};
		});

		services.AddScoped<IAuthenticationService, AuthenticationService>();
		services.AddScoped<IGoogleJsonWebSignature, GoogleJsonWebSignatureWrapper>();
		services.AddScoped<JwtSecurityTokenHandler>();

		return services;
	}
}
