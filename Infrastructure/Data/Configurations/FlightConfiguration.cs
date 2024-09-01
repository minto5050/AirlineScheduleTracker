using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
	public void Configure(EntityTypeBuilder<Flight> builder)
	{
		builder.ToTable("flights");
		builder.HasOne(x => x.Route)
			.WithMany(x=>x.Flights)
			.HasForeignKey(x=> x.RouteId );
		builder.Property(x => x.FlightId)
			.HasColumnName("flight_id");
		builder.HasKey(x => x.FlightId);
		builder.Property(x => x.RouteId)
			.HasColumnName("route_id");
		builder.Property(x => x.ArrivalTime)
			.HasColumnName("arrival_time");
		builder.Property(x => x.DepartureTime)
			.HasColumnName("departure_time");
		builder.Property(x => x.AirlineId)
			.HasColumnName("airline_id");
		//Index creation
		//Ignoring the PK and FK since it will be created by EFCore
		builder.HasIndex(x => x.DepartureTime);
	}
}
