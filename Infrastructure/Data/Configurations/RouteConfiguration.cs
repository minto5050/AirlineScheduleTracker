using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
	public void Configure(EntityTypeBuilder<Route> builder)
	{
		builder.ToTable("routes");
		builder.Property(x => x.RouteId).HasColumnName("route_id");
		builder.Property(x => x.OriginCityId).HasColumnName("origin_city_id");
		builder.Property(x => x.DestinationCityId).HasColumnName("destination_city_id");
		builder.Property(x=>x.DepartureDate).HasColumnName("departure_date");

		builder.HasKey(x => x.RouteId);
		builder.HasMany(x => x.Flights)
			.WithOne(x => x.Route)
			.HasForeignKey(x => x.RouteId);
		//Indexes

		builder.HasIndex(x => new { x.OriginCityId,x.DestinationCityId});
	}
}
