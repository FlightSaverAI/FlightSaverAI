import dayjs from 'dayjs';

import { Component, input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import L from 'leaflet';

@Component({
  selector: 'home-map',
  standalone: true,
  imports: [CommonModule],
  template: `<div id="map"></div>`,
  styleUrl: './map.component.scss',
})
export class MapComponent implements OnInit {
  markerIcon = input.required<any>();
  flightData = input.required<any[]>();
  flightsMap: any;

  public ngOnInit() {
    this._initMap();
  }

  private _initMap() {
    this.flightsMap = L.map('map').setView([52.2297, 21.0122], 5);

    L.tileLayer('https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png', {
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors &copy; <a href="https://carto.com/attributions">CARTO</a>',
      subdomains: 'abcd',
      maxZoom: 20,
    }).addTo(this.flightsMap);

    this.flightData().forEach((flight) => {
      const departureLatLng = [flight.departureAirportLatitude, flight.departureAirportLongitude];
      const arrivalLatLng = [flight.arrivalAirportLatitude, flight.arrivalAirportLongitude];

      const polyline = L.polyline([departureLatLng, arrivalLatLng], { color: 'red' }).addTo(
        this.flightsMap
      );

      this.flightsMap.fitBounds(polyline.getBounds());
      this.flightsMap.setZoom(4);

      this._addMarker(
        departureLatLng,
        `<strong>Start:</strong> ${
          flight.departureAirportName
        } <br/> <strong>Departure Time:</strong> ${dayjs(flight.departureTime).format(
          'DD/MM/YYYY HH:mm'
        )}`
      );
      this._addMarker(
        arrivalLatLng,
        `<strong>End:</strong> ${
          flight.arrivalAirportName
        } <br/> <strong>Arrival Time:</strong> ${dayjs(flight.arrivalTime).format(
          'DD/MM/YYYY HH:mm'
        )}`
      );
    });
  }

  private _addMarker(latLng: number[], popupText: string) {
    L.marker([latLng[0], latLng[1]], { icon: this.markerIcon() })
      .addTo(this.flightsMap)
      .bindPopup(popupText);
  }
}
