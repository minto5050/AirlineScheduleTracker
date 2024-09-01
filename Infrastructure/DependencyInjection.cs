using Application.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfraLayer(this IServiceCollection services,
		IConfiguration configuration)
	{
			services.AddDbContext<AirlineScheduleDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(AirlineScheduleDbContext).Assembly.FullName)));
		services.AddScoped<AirlineScheduleDataInitializer>();
		services.AddScoped<IAirlineScheduleDbContext, AirlineScheduleDbContext>();
		return services;
	}

	public static async Task InitializeDb(this AirlineScheduleApp app)
	{
		using var scope = app.services.CreateScope();

		var initialiser = scope.ServiceProvider.GetRequiredService<AirlineScheduleDataInitializer>();

		await initialiser.Initialize();

		await initialiser.Seed(CancellationToken.None);
	}
}
