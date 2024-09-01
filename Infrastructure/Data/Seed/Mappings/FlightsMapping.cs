using CsvHelper.Configuration;
using Domain.Entities;

namespace Infrastructure.Data.Seed.Mappings;

internal sealed class FlightsMapping : ClassMap<Flight>
{
    public FlightsMapping()
    {
        Map(x => x.ArrivalTime).Name("arrival_time");
        Map(x => x.DepartureTime).Name("departure_time");
        Map(x => x.AirlineId).Name("airline_id");
        Map(x => x.FlightId).Name("flight_id");
        Map(x => x.RouteId).Name("route_id");

    }
}
