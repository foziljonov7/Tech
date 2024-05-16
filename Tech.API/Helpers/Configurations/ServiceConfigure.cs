using Microsoft.EntityFrameworkCore;
using Tech.DAL.DbContexts;

namespace Tech.API.Helpers.Configurations;

public static class ServiceConfigure
{
	public static IServiceCollection AddDbConfigure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("localhost");

		services.AddDbContext<AppDbContext>(options
			=> options.UseNpgsql(connectionString));

		return services;
	}
}
