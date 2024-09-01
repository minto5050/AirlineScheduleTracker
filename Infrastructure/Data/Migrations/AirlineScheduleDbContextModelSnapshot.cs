﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AirlineScheduleDbContext))]
    partial class AirlineScheduleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("flight_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"));

                    b.Property<int>("AirlineId")
                        .HasColumnType("int")
                        .HasColumnName("airline_id");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("arrival_time");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("departure_time");

                    b.Property<int>("RouteId")
                        .HasColumnType("int")
                        .HasColumnName("route_id");

                    b.HasKey("FlightId");

                    b.HasIndex("DepartureTime");

                    b.HasIndex("RouteId");

                    b.ToTable("flights", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("route_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteId"));

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("departure_date");

                    b.Property<int>("DestinationCityId")
                        .HasColumnType("int")
                        .HasColumnName("destination_city_id");

                    b.Property<int>("OriginCityId")
                        .HasColumnType("int")
                        .HasColumnName("origin_city_id");

                    b.HasKey("RouteId");

                    b.HasIndex("OriginCityId", "DestinationCityId");

                    b.ToTable("routes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Subscription", b =>
                {
                    b.Property<int>("AgencyId")
                        .HasColumnType("int")
                        .HasColumnName("agency_id");

                    b.Property<int>("DestinationCityId")
                        .HasColumnType("int")
                        .HasColumnName("destination_city_id");

                    b.Property<int>("OriginCityId")
                        .HasColumnType("int")
                        .HasColumnName("origin_city_id");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.HasIndex("AgencyId");

                    b.HasIndex("RouteId");

                    b.HasIndex("OriginCityId", "DestinationCityId");

                    b.ToTable("subscriptions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Flight", b =>
                {
                    b.HasOne("Domain.Entities.Route", "Route")
                        .WithMany("Flights")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Domain.Entities.Subscription", b =>
                {
                    b.HasOne("Domain.Entities.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.Navigation("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}
