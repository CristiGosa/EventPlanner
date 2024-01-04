using EventPlanner.Domain.Repositories;
using EventPlanner.Database.Context;
using EventPlanner.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MySqlConnector;

using System.Reflection;

namespace EventPlanner.Database;

public static class ServiceExtension
{
	private const string ConnectionStringKey = "MySql";
	private const string ConnectionStringPasswordKey = "MySqlPassword";

	public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString(ConnectionStringKey);

        if (connectionString is null)
		{
			throw new Exception($"Cannot find {ConnectionStringKey} key for connection string");
		}

		var connectionStringPassword = configuration.GetSection(ConnectionStringPasswordKey).Value;
		if (connectionStringPassword is null)
		{
			throw new Exception($"Cannot find {ConnectionStringPasswordKey} key for connection string password");
		}

		var conStrBuilder = new MySqlConnectionStringBuilder(connectionString) { Password = connectionStringPassword };
		return services.AddDbContext<DataContext>(opt =>
			{
				var serverVersion = new MySqlServerVersion(new Version(8, 2, 0));
				opt.UseMySql(conStrBuilder.ConnectionString, serverVersion)
					.EnableDetailedErrors();
			})
			.AddScoped<IUnitOfWork, UnitOfWork>()
			.AddScoped<IUserRepository, UserRepository>()
			.AddScoped<ILocationRepository, LocationRepository>()
			.AddScoped<IEventRepository, EventRepository>()
			.AddAutoMapper(Assembly.GetExecutingAssembly());
	}
}