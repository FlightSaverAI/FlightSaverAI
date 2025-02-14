import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SliderCardComponent } from '@shared/ui-components';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';
import { ActivityOverviewChartConfig } from '@flight-saver/statistics/models';

@Component({
  selector: 'statistics-activity-overview',
  standalone: true,
  imports: [CommonModule, SliderCardComponent, NgxEchartsDirective],
  template: ` <shared-slider-card class="u-justify-center">
    <div class="card" *ngFor="let statistic of activityOverview()">
      <img class="card__image" [src]="statistic.imageSrc" />
      <div class="card__title">
        <p>{{ statistic.cardTitle }}</p>
      </div>
      <div class="card__chart-container">
        <div class="card__chart" echarts [options]="statistic"></div>
      </div>
    </div>
  </shared-slider-card>`,
  styleUrl: './activity-overview.component.scss',
  providers: [provideEcharts()],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ActivityOverviewComponent {
  public activityOverview = input.required<ActivityOverviewChartConfig[]>();
}
