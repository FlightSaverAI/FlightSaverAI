import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SliderCardComponent } from '@shared/ui-components';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';

@Component({
  selector: 'statistics-activity-overview',
  standalone: true,
  imports: [CommonModule, SliderCardComponent, NgxEchartsDirective],
  template: ` <shared-slider-card style="display: flex; justify-content: center">
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
})
export class ActivityOverviewComponent {
  activityOverview = input.required<any>();
}
