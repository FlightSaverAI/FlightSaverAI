import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';
import { SliderCardComponent } from '@shared/ui-components';

@Component({
  selector: 'statistics-flight-passenger-overview',
  standalone: true,
  imports: [CommonModule, NgxEchartsDirective, SliderCardComponent],
  template: `
    <shared-slider-card style="display: flex; justify-content: center">
      <div class="card" *ngFor="let statistic of flightPassangerOverview()">
        <div class="card__chart" echarts [options]="statistic"></div>
      </div>
    </shared-slider-card>
  `,
  styleUrl: './flight-passenger-overview.component.scss',
  providers: [provideEcharts()],
})
export class FlightPassengerOverviewComponent {
  flightPassangerOverview = input.required<any>();
}
