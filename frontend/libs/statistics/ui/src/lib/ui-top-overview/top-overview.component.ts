import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SliderCardComponent } from '@shared/ui-components';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';

@Component({
  selector: 'statistics-top-overview',
  standalone: true,
  imports: [CommonModule, SliderCardComponent, NgxEchartsDirective],
  template: ` <shared-slider-card style="display: flex; justify-content: center">
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
})
export class TopOverviewComponent {
  topOverview = input.required<any>();
  // options = input.required();
}
