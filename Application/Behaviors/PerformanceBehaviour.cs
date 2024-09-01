using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace Application.Behaviors;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
	private readonly Stopwatch _timer;
	private readonly ILogger<TRequest> _logger;

	public PerformanceBehaviour(
		ILogger<TRequest> logger)
	{
		_timer = new Stopwatch();

		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		_timer.Start();

		var response = await next();

		_timer.Stop();

		var elapsedMilliseconds = _timer.ElapsedMilliseconds;
		var requestName = typeof(TRequest).Name;
		_logger.LogWarning("{Name} : {ElapsedMilliseconds} ms",
				requestName, elapsedMilliseconds);

		if (elapsedMilliseconds > 500)
		{
			_logger.LogWarning("🐌⚠️: {Name} ({ElapsedMilliseconds} milliseconds)",
				requestName, elapsedMilliseconds);
		}
		return response;
	}
}