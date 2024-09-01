using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Infrastructure.Data;

public class AirlineScheduleDataInitializer
{
	private readonly AirlineScheduleDbContext _dbContext;
	private readonly ILogger<AirlineScheduleDataInitializer> _logger;

	public AirlineScheduleDataInitializer(AirlineScheduleDbContext airlineScheduleDbContext, ILogger<AirlineScheduleDataInitializer> logger)
	{
		_dbContext = airlineScheduleDbContext;
		_logger = logger;
	}
	public async Task Initialize()
	{
		try
		{
			await _dbContext.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error initializing the db");
			throw;
		}
	}
	public async Task Seed(CancellationToken token)
	{
		var seedTimer = new Stopwatch();
		int inserted = 0;
		try
		{
			seedTimer.Start();
			if(!_dbContext.Routes.Any())
			{
				_logger.LogInformation("seeding routes 🛣️");
				await _dbContext.Routes
					.AddRangeAsync(new SeedHelper<Route>()
					.LoadCsv("./Data/Seed/Assets/routes.csv"));
				_logger.LogInformation("🛣️ : {ellapsed}", seedTimer.ElapsedMilliseconds);
				inserted += await _dbContext.SaveChangesWithIdentityInsert<Route>(token);
			}
			if (!_dbContext.Flights.Any())
			{
				_logger.LogInformation("seeding flights ✈️");
				await _dbContext.Flights
					.AddRangeAsync(new SeedHelper<Flight>()
					.LoadCsv("./Data/Seed/Assets/flights.csv"));
				_logger.LogInformation("✈️ : {ellapsed}", seedTimer.ElapsedMilliseconds);
				inserted += await _dbContext.SaveChangesWithIdentityInsert<Flight>(token);
			}
			if (_dbContext.Subscriptions.Any())
			{
				_logger.LogInformation("seeding subscriptions 💵");
				await _dbContext.Subscriptions
					.AddRangeAsync(new SeedHelper<Subscription>()
					.LoadCsv("./Data/Seed/Assets/subscriptions.csv"));
				_logger.LogInformation("💵 : {ellapsed}", seedTimer.ElapsedMilliseconds);
				inserted += await _dbContext.SaveChangesAsync(token);
			}
			
			if (inserted > 0)
			{
				_logger.LogInformation("Completed seed in {seedElapsed}",seedTimer.ElapsedMilliseconds);
			}
		}catch(Exception ex)
		{
			_logger.LogError(ex, "failed to insert seed data");
		}
		finally
		{
			seedTimer.Stop();
		}
	}
}
