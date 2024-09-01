using ConsoleUI.Abstracts;
using CsvHelper;
using Domain.Dtos;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace ConsoleUI.Concretes;

public class CsvOutputService : IOutputService
{
	private readonly ILogger<CsvOutputService> _logger;

	public CsvOutputService(ILogger<CsvOutputService> logger)
	{
		_logger = logger;
	}
	public Task<bool> Write(ICollection<FlightSchedule> flights, string Destination)
	{
		try
		{
			using (var writer = new StreamWriter(Destination))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(flights);
			}
			_logger.LogInformation("{count} results written to {filePath}", flights.Count, Destination);
			return Task.FromResult(true);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error trying to write to CSV file");
		}
		return Task.FromResult(false);
	}
}
