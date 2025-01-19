import { ChangeDetectionStrategy, Component, computed, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormatTimePipe } from './format-time.pipe';

@Component({
  selector: 'home-flights-summary',
  standalone: true,
  imports: [CommonModule, FormatTimePipe],
  template: `<div class="summary">
    <div class="summary__flights">
      <img src="global/assets/flight.svg" alt="" />
      <div class="summary__stats">
        <p>
          <strong>{{ this.statistics().flightCount.count }}</strong> flights
        </p>
        <p>
          <strong>{{ this.statistics().flightCount.flightDistribution.International }}</strong>
          international
        </p>
        <p>
          <strong>{{ this.statistics().flightCount.flightDistribution.Domestic }}</strong>
          domestic
        </p>
      </div>
    </div>
    <div class="summary__flights">
      <img src="global/assets/distance.svg" alt="" />
      <div class="summary__stats">
        <p>
          <strong>{{ totalDistance() }}</strong> km
        </p>
        <p>
          <strong>{{ this.statistics().distance.aroundEarthDistance.toFixed(2) }}x</strong> around
          the earth
        </p>
        <p>
          <strong>{{ this.statistics().distance.toTheMoonDistance.toFixed(2) }}x</strong> to the
          moon
        </p>
      </div>
    </div>
    <div class="summary__flights">
      <img src="global/assets/time.svg" alt="" />
      <div class="summary__stats">
        <p>
          <strong>{{ this.statistics().totalFlightTime.time | formatTime }}</strong>
        </p>
        <p>
          <strong>{{ this.statistics().totalFlightTime.days.toFixed(2) }}</strong> days
        </p>
        <p>
          <strong>{{ this.statistics().totalFlightTime.months.toFixed(2) }}</strong> months
        </p>
      </div>
    </div>
  </div>`,
  styleUrl: './flights-summary.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FlightsSummaryComponent {
  statistics = input.required<any>();
  totalDistance = computed(() => Math.round(this.statistics().distance.totalDistance));
}
