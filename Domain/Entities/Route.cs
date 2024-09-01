﻿namespace Domain.Entities;

public class Route
{
    public int RouteId { get; set; }
    public int OriginCityId { get; set; }
    public int DestinationCityId { get; set; }
    public DateTime DepartureDate { get; set; }
    public virtual ICollection<Flight>? Flights { get; set; }

}
