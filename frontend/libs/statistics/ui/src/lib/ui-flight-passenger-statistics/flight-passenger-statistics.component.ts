import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';
import { SliderCardComponent } from '@shared/ui-components';

@Component({
  selector: 'statistics-flight-passenger-statistics',
  standalone: true,
  imports: [CommonModule, NgxEchartsDirective, SliderCardComponent],
  template: `
    <shared-slider-card style="display: flex; justify-content: center; margin-top: 20px">
      <div class="card" *ngFor="let statistic of flightPassangerStatistics()">
        <div echarts [options]="statistic"></div>
      </div>
    </shared-slider-card>
  `,
  styleUrl: './flight-passenger-statistics.component.scss',
  providers: [provideEcharts()],
})
export class FlightPassengerStatisticsComponent {
  flightPassangerStatistics = input.required<any>();
}
