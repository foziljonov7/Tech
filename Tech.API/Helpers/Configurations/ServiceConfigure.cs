using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tech.DAL.DbContexts;
using Tech.Infrastructure.Interfaces;
using Tech.Infrastructure.Repositories;
using Tech.Services.Interfaces.Users;
using Tech.Services.Services.Users;

namespace Tech.API.Helpers.Configurations;

public static class ServiceConfigure
{
	public static IServiceCollection AddDbConfigure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("localhost");
		
		//DbContext configuration
		services.AddDbContext<AppDbContext>(options
			=> options.UseNpgsql(connectionString));

		return services;
	}

	public static IServiceCollection AddServiceConfigure(
		this IServiceCollection services)
	{
		//Repository configuration
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

		//Services configuration
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IAccountService, AccountService>();

		return services;
	}
}
