import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightPassengerOverviewComponent } from '@flight-saver/statistics/ui';
import { TopOverviewComponent } from '@flight-saver/statistics/ui';
import { ActivityOverviewComponent } from '@flight-saver/statistics/ui';
import {
  createActivityOverviewChartConfig,
  createFlightOverviewChartConfig,
  createTopOverviewChartConfig,
} from '@flight-saver/statistics/utils';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    FlightPassengerOverviewComponent,
    TopOverviewComponent,
    ActivityOverviewComponent,
  ],
  template: `
    <div class="wrapper">
      <statistics-flight-passenger-overview
        [flightPassangerOverview]="flightOverviewChartConfig()"
      ></statistics-flight-passenger-overview>
      <statistics-top-overview [topOverview]="topOverviewChartConfig()"></statistics-top-overview>
      <statistics-activity-overview
        [activityOverview]="activityOverviewChartConfig()"
      ></statistics-activity-overview>
    </div>
  `,
  styleUrl: './statistics.component.scss',
})
export class StatisticsComponent {
  flightOverviewChartConfig = toSignal(createFlightOverviewChartConfig());
  topOverviewChartConfig = toSignal(createTopOverviewChartConfig());
  activityOverviewChartConfig = toSignal(createActivityOverviewChartConfig());
}
