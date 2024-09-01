using Domain.Dtos;

namespace ConsoleUI.Abstracts;

public interface IOutputService
{
	Task<bool> Write(ICollection<FlightSchedule> flights,string Destination);
}
