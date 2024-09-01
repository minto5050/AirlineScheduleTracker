using Application.Flights.Queries;
using ConsoleUI.Abstracts;
using ConsoleUI.Models;
using MediatR;

namespace ConsoleUI.Concretes;

internal class BackboneService : IBackboneService
{
	private readonly ISender _sender;
	private readonly IOutputService _outputService;

	public BackboneService(ISender sender,IOutputService opService)
    {
		_sender = sender;
		_outputService = opService;
    }

	public async Task Execute(Options option)
	{
		var query = new GetFlightsQuery(option.AgencyId, option.StartDate, option.EndDate);
		var result = await _sender.Send(query);
		_ = _outputService.Write(result, option.OutputPath);
	}
}
