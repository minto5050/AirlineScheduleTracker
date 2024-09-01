using CsvHelper.Configuration;
using Domain.Entities;

namespace Infrastructure.Data.Seed.Mappings;

internal sealed class SubscriptionsMapping : ClassMap<Subscription>
{
    public SubscriptionsMapping()
    {
        Map(x => x.OriginCityId).Name("origin_city_id");
        Map(x => x.AgencyId).Name("agency_id");
        Map(x => x.DestinationCityId).Name("destination_city_id");
	}
}
