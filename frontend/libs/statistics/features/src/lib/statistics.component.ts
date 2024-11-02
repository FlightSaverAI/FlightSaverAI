import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FlightPassengerOverviewComponent } from '@flight-saver/statistics/ui';
import { TopOverviewComponent } from '@flight-saver/statistics/ui';
import { StatisticsContants } from './constants/statistics.constants';

@Component({
  standalone: true,
  imports: [CommonModule, FlightPassengerOverviewComponent, TopOverviewComponent],
  template: `<statistics-flight-passenger-overview
      [flightPassangerOverview]="flightPassangerOverview"
    ></statistics-flight-passenger-overview>
    <statistics-top-overview [topOverview]="topOverview"></statistics-top-overview> `,
  styleUrl: './statistics.component.scss',
})
export class StatisticsComponent {
  flightPassangerOverview = StatisticsContants.flightPassangerOverview;
  topOverview = StatisticsContants.topOverview;
}
