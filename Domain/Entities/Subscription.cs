namespace Domain.Entities;

public class Subscription
{
    public int AgencyId { get; set; }
    public int OriginCityId { get; set; }
    public int DestinationCityId { get; set; }
	public virtual Route Route { get; set; }
	//public virtual Route Route { get; set; }
}
