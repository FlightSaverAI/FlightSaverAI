import { Component } from '@angular/core';
import { MapComponent } from '@flight-saver/home/ui';
import { FlightsSummaryComponent } from '@flight-saver/home/ui';

import L from 'leaflet';

@Component({
  standalone: true,
  imports: [MapComponent, FlightsSummaryComponent],
  template: `
    <div class="wrapper">
      <home-map [markerIcon]="markerIcon" [flightData]="flightData"></home-map>
      <home-flights-summary></home-flights-summary>
    </div>
  `,
  styleUrl: './home-container.component.scss',
})
export class HomeContainerComponent {
  markerIcon = L.icon({
    iconUrl: 'global/assets/images/marker-icon.png',
    iconSize: [40, 40],
    iconAnchor: [20, 38],
  });

  flightData = [
    {
      start: {
        departureAirport: 'Warsaw / Okęcie',
        latLng: [52.2297, 21.0122],
      },
      end: {
        arrivalAirport: 'Berlin',
        latLng: [52.52, 13.405],
      },
    },
  ];
}
