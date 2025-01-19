import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { HomeService } from '@flight-saver/home/data-access';
import { MapComponent } from '@flight-saver/home/ui';
import { FlightsSummaryComponent } from '@flight-saver/home/ui';

import L from 'leaflet';

@Component({
  standalone: true,
  imports: [MapComponent, FlightsSummaryComponent],
  template: `
    @defer(when flightData()){
    <div class="wrapper">
      <home-map [markerIcon]="markerIcon()" [flightData]="flightData()"></home-map>
      @defer(when basicStatistics()){
      <home-flights-summary [statistics]="basicStatistics()"></home-flights-summary>
      }
    </div>
    }
  `,
  styleUrl: './home-container.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeContainerComponent {
  protected markerIcon = signal(
    L.icon({
      iconUrl: 'global/assets/images/marker-icon.png',
      iconSize: [40, 40],
      iconAnchor: [20, 38],
    })
  );

  private _homeService = inject(HomeService);

  protected flightData = toSignal(this._homeService.getFlights());
  protected basicStatistics = toSignal(this._homeService.getBasicStatistics());
}
