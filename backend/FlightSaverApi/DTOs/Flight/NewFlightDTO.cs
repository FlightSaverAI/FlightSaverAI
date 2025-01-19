using System.Text.Json.Serialization;

namespace FlightSaverApi.DTOs.Flight;

public class NewFlightDTO
{
    public FlightDetailsForm flightDetailsForm {get; set;}
    public int? airlineId {get; set;}
    public int? aircraftId {get; set;}
    public TicketForm ticketForm {get; set;}
    public RateAndReviewForm? rateAndReviewForm { get; set; }
    [JsonIgnore]
    public int? UserId { get; set; }
}

public class FlightDetailsForm
{
    public DateTime departureDate { get; set; }
    public int departureAirportId { get; set; }
    public int arrivalAirportId { get; set; }
    public string? flightNumber { get; set; }
    public int departureTimeHour { get; set; }
    public int departureTimeMinutes { get; set; }
    public int arrivalTimeHour { get; set; }
    public int arrivalTimeMinutes { get; set; }
    public int flightDurationHour { get; set; }
    public int flightDurationMinutes { get; set; }
}

public class TicketForm
{
    public string @class { get; set; } = string.Empty;
    public string seat { get; set; } = string.Empty;
    public string seatNumber { get; set; } = string.Empty;
    public string reason { get; set; } = string.Empty;
}

public class RateAndReviewForm
{
    public DepartureAirportOpinion? departureAirportOpinion { get; set; }
    public ArrivalAirportOpinion? arrivalAirportOpinion { get; set; }
    public AirlineOpinion? airlinesOpinion { get; set; }
    public AircraftOpinion? airPlaneOpinion { get; set; }
}

public class DepartureAirportOpinion
{
    public int rate { get; set; }
    public string review { get; set; } = string.Empty;
}

public class ArrivalAirportOpinion
{
    public int rate { get; set; }
    public string review { get; set; } = string.Empty;
}

public class AirlineOpinion
{
    public int rate { get; set; }
    public string review { get; set; } = string.Empty;
}

public class AircraftOpinion
{
    public int rate { get; set; }
    public string review { get; set; } = string.Empty;
}