using Application.Abstractions;
using Domain.Constants;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Application.Flights.Queries;

public record GetFlightsQuery(int AgencyId,DateTime Start,DateTime End) : IRequest<ICollection<FlightSchedule>>;

public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, ICollection<FlightSchedule>>
{
	private readonly IAirlineScheduleDbContext _dbContext;

	private readonly ILogger<GetFlightsQueryHandler> _logger;

	public GetFlightsQueryHandler(IAirlineScheduleDbContext dbContext,
		ILogger<GetFlightsQueryHandler> logger)
    {
        _dbContext = dbContext;
		_logger = logger;
    }
    public async Task<ICollection<FlightSchedule>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Read complete");
		var tolerance = TimeSpan.FromMinutes(30);
		var daysTolerance = TimeSpan.FromDays(7);
		var flights = _dbContext.Flights
				.Join(
					_dbContext.Routes,
					f => f.RouteId,
					r => r.RouteId,
					(f, r) => new { f, r }
				)
				.Join(
					_dbContext.Subscriptions,
					fr => new { fr.r.OriginCityId, fr.r.DestinationCityId },
					s => new { s.OriginCityId, s.DestinationCityId },
					(fr, s) => new { fr.f, fr.r, s }
				)
				.Where(x => x.s.AgencyId == request.AgencyId
							&& x.f.DepartureTime >= request.Start
							&& x.f.DepartureTime <= request.End)
				.Select(x => new FlightSchedule()
				{
					FlightId = x.f.FlightId,
					OriginCityId = x.r.OriginCityId,
					DestinationCityId = x.r.DestinationCityId,
					DepartureTime = x.f.DepartureTime,
					ArrivalTime = x.f.ArrivalTime,
					AirlineId = x.f.AirlineId,
					Status = (from f2 in _dbContext.Flights
							  where f2.AirlineId == x.f.AirlineId
							  && (f2.DepartureTime >= x.f.DepartureTime.AddDays(-7).AddMinutes(-30) && f2.DepartureTime <= x.f.DepartureTime.AddDays(7).AddMinutes(30))
							  && f2.DepartureTime != x.f.DepartureTime
							  select f2).Any()
				? "Existing"
				: (from f3 in _dbContext.Flights
				   where f3.AirlineId == x.f.AirlineId
				   && f3.DepartureTime > x.f.DepartureTime
				   select f3).Any()
				? "Discontinued"
				: "New"
				})
				.ToList();
		_logger.LogInformation("DB read completed, {entries} entries", flights.Count);
		return flights;
	}
	private string GetFlightStatus(dynamic flight, List<dynamic> allFlights, TimeSpan tolerance, TimeSpan daysTolerance)
	{
		_logger.LogInformation("processing status for {flightId}",(int)flight.FlightId);
		bool hasFlightWithinMinus7Days = allFlights.Any(f =>
			f.AirlineId == flight.AirlineId &&
			f.DepartureTime != flight.DepartureTime &&
			f.DepartureTime >= flight.DepartureTime - daysTolerance - tolerance &&
			f.DepartureTime <= flight.DepartureTime - daysTolerance + tolerance
		);

		bool hasFlightWithinPlus7Days = allFlights.Any(f =>
			f.AirlineId == flight.AirlineId &&
			f.DepartureTime != flight.DepartureTime &&
			f.DepartureTime >= flight.DepartureTime + daysTolerance - tolerance &&
			f.DepartureTime <= flight.DepartureTime + daysTolerance + tolerance
		);

		if (!hasFlightWithinMinus7Days)
		{
			return FlightStatus.NEW;
		}
		else if (!hasFlightWithinPlus7Days)
		{
			return FlightStatus.DISCONTINUED;
		}
		return string.Empty;
	}
}