using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
	public void Configure(EntityTypeBuilder<Subscription> builder)
	{
		builder.ToTable("subscriptions");
		builder.HasNoKey();
		
		builder.Property(x => x.OriginCityId).HasColumnName("origin_city_id");
		builder.Property(x => x.DestinationCityId).HasColumnName("destination_city_id");
		builder.Property(x => x.AgencyId).HasColumnName("agency_id");
		
		//Index creation
		builder.HasIndex(x => x.AgencyId);
		builder.HasIndex(x => new { x.OriginCityId, x.DestinationCityId });
	}
}
