using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class AirlineScheduleDbContext : DbContext, IAirlineScheduleDbContext
{
    public AirlineScheduleDbContext()
    {
        
    }
    public AirlineScheduleDbContext(DbContextOptions<AirlineScheduleDbContext> options):base(options)
	{
	}

	public DbSet<Route> Routes { get; init; }
	public DbSet<Flight> Flights { get; init; }
	public DbSet<Subscription> Subscriptions { get; init; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
	public async Task<int> SaveChangesWithIdentityInsert<TEnt>(CancellationToken token = default)
	{
		await using var transaction = await Database.BeginTransactionAsync(token);
		await SetIdentityInsertAsync<TEnt>(true, token);
		int ret = await SaveChangesAsync(token);
		await SetIdentityInsertAsync<TEnt>(false, token);
		await transaction.CommitAsync(token);

		return ret;
	}

	private async Task SetIdentityInsertAsync<TEnt>(bool enable, CancellationToken token)
	{
		var entityType = Model.FindEntityType(typeof(TEnt));
		var value = enable ? "ON" : "OFF";
		string query = $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}";
		await Database.ExecuteSqlRawAsync(query, token);
	}
}
