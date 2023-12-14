using FluentValidation;

using EventPlanner.Business.Common.Behavior;
using EventPlanner.Domain.Options;

using MediatR.Pipeline;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace EventPlanner.Business;

public static class ServiceExtension
{
	public static IServiceCollection ConfigureBusiness(this IServiceCollection services, IConfiguration configuration)
	{
		return services.AddAutoMapper(Assembly.GetExecutingAssembly())
			.AddMediatR(x =>
			{
				x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
					.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcess<>));
			})
		.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
			.Configure<InitialUsers>(configuration.GetSection(nameof(InitialUsers)));
	}
}