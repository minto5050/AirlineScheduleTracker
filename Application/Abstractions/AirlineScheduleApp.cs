using Microsoft.Extensions.DependencyInjection;

namespace Application.Abstractions
{
	public abstract class AirlineScheduleApp
	{
		public string verbosity;
		public IServiceScopeFactory services;

	}
}
