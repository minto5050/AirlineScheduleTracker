using Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddAppLayer(this IServiceCollection services)
		{
			services.AddMediatR(cfg => {
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
			});
			return services;
		}
	}
}
