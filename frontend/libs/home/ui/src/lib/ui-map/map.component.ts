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

  ngOnInit() {
    this._initMap();
  }

  private _initMap() {
    this.flightsMap = L.map('map').setView([52.2297, 21.0122], 6);

    L.tileLayer('https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png', {
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors &copy; <a href="https://carto.com/attributions">CARTO</a>',
      subdomains: 'abcd',
      maxZoom: 20,
    }).addTo(this.flightsMap);

    this.flightData().forEach((flight) => {
      const polyline = L.polyline([flight.start.latLng, flight.end.latLng], { color: 'red' }).addTo(
        this.flightsMap
      );

      this.flightsMap.fitBounds(polyline.getBounds());

      this._addMarker(flight.start.latLng, `Start: ${flight.start.departureAirport}`);
      this._addMarker(flight.end.latLng, `End: ${flight.end.arrivalAirport}`);
    });
  }

  private _addMarker(latLng: number[], popupText: string) {
    L.marker([latLng[0], latLng[1]], { icon: this.markerIcon() })
      .addTo(this.flightsMap)
      .bindPopup(popupText);
  }
}
