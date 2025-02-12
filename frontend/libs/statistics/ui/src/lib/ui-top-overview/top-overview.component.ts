import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SliderCardComponent } from '@shared/ui-components';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';
import { TopOverviewChartConfig } from '@flight-saver/statistics/models';

@Component({
  selector: 'statistics-top-overview',
  standalone: true,
  imports: [CommonModule, SliderCardComponent, NgxEchartsDirective],
  template: ` <shared-slider-card class="u-justify-center">
    <div class="card" *ngFor="let statistic of topOverview()">
      <div class="card__image-container">
        <img class="card__image" [src]="statistic.imageSrc" alt="" />
        <div class="card__title">
          <p>{{ statistic.cardTitle }}</p>
        </div>
      </div>
      <div class="card__chart-container">
        <div class="card__chart" echarts [options]="statistic"></div>
      </div>
    </div>
  </shared-slider-card>`,
  styleUrl: './top-overview.component.scss',
  providers: [provideEcharts()],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TopOverviewComponent {
  public topOverview = input.required<TopOverviewChartConfig[]>();
}
