using Application.Abstractions;
using ConsoleUI.Abstracts;
using ConsoleUI.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI;

internal class ConsoleUIApp : AirlineScheduleApp
{
	private readonly IBackboneService _backboneService;

	public ConsoleUIApp(IBackboneService backboneService,
		IServiceScopeFactory services)
    {
        this._backboneService = backboneService;
		base.services = services;

    }
    public Task Run(Options options)
	{
		base.verbosity = options.Verbosity;
		_backboneService.Execute(options);
		return Task.CompletedTask;
	}
}
