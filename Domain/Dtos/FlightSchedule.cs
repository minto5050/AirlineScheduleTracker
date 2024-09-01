﻿namespace Domain.Dtos;

public class FlightSchedule
{
    public int FlightId { get; set; }
    public int OriginCityId { get; set; }
    public int DestinationCityId { get; set; }
    public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }
    public int AirlineId { get; set; }
    public string Status { get; set; }

}
