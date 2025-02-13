export interface Flight {
  id: number;
  flightNumber: string;
  departureAirportName: string;
  departureAirportLatitude: number;
  departureAirportLongitude: number;
  arrivalAirportName: string;
  arrivalAirportLatitude: number;
  arrivalAirportLongitude: number;
  departureTime: string;
  arrivalTime: string;
}
