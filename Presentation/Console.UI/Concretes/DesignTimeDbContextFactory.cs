using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleUI.Concretes;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AirlineScheduleDbContext>
{
	public AirlineScheduleDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<AirlineScheduleDbContext>();
		optionsBuilder.UseSqlServer(args[1]);

		return new AirlineScheduleDbContext(optionsBuilder.Options);
	}
}
