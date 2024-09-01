namespace Domain.Entities;

public class Flight
{
	public int FlightId { get; set; }
    public int RouteId { get; set; }

    public virtual Route Route { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int AirlineId{ get; set; }

}
