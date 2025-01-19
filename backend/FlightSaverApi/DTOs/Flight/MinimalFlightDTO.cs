namespace FlightSaverApi.DTOs.Flight;

public class MinimalFlightDTO
{
    public int Id { get; set; }

    public string FlightNumber { get; set; }
    
    public string DepartureAirportName { get; set; }
        
    public double DepartureAirportLatitude { get; set; }
    
    public double DepartureAirportLongitude { get; set; }
    
    public string ArrivalAirportName { get; set; }
    
    public double ArrivalAirportLatitude { get; set; }
    
    public double ArrivalAirportLongitude { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }
}