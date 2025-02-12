import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';
import { SliderCardComponent } from '@shared/ui-components';
import { EChartsOption } from 'echarts';

@Component({
  selector: 'statistics-flight-passenger-overview',
  standalone: true,
  imports: [CommonModule, NgxEchartsDirective, SliderCardComponent],
  template: `
    <shared-slider-card class="u-justify-center">
      <div class="card" *ngFor="let statistic of flightPassangerOverview()">
        <div class="card__chart" echarts [options]="statistic"></div>
      </div>
    </shared-slider-card>
  `,
  styleUrl: './flight-passenger-overview.component.scss',
  providers: [provideEcharts()],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FlightPassengerOverviewComponent {
  public flightPassangerOverview = input.required<EChartsOption[]>();
}
