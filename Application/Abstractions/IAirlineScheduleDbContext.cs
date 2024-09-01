using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IAirlineScheduleDbContext
{
    public DbSet<Route> Routes { get; init; }

    public DbSet<Flight> Flights { get; init; }

    public DbSet<Subscription> Subscriptions { get; init; }
}
