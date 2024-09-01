using CsvHelper.Configuration;
using Domain.Entities;

namespace Infrastructure.Data.Seed.Mappings;

internal sealed class RoutesMapping : ClassMap<Route>
{
	public RoutesMapping()
	{
		Map(x => x.OriginCityId).Name("origin_city_id");
		Map(x => x.RouteId).Name("route_id");
		Map(x => x.DestinationCityId).Name("destination_city_id");
		Map(x => x.DepartureDate).Name("departure_date");
	}
}
