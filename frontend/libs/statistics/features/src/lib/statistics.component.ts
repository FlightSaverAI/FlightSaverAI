import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FlightPassengerStatisticsComponent } from '@flight-saver/statistics/ui';
import { TopStatisticsComponent } from '@flight-saver/statistics/ui';
import { StatisticsContants } from './constants/statistics.constants';

@Component({
  standalone: true,
  imports: [CommonModule, FlightPassengerStatisticsComponent, TopStatisticsComponent],
  template: `<statistics-flight-passenger-statistics
      [flightPassangerStatistics]="flightPassangerOverview"
    ></statistics-flight-passenger-statistics>
    <statistics-top-statistics [topStatistics]="topOverview"></statistics-top-statistics> `,
  styleUrl: './statistics.component.scss',
})
export class StatisticsComponent {
  flightPassangerOverview = StatisticsContants.flightPassangerOverview;
  topOverview = StatisticsContants.topOverview;
}
