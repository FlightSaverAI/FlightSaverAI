import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'home-flights-summary',
  standalone: true,
  imports: [CommonModule],
  template: ` <!-- MOCKED DATA -->
    <div class="summary">
      <div class="summary__flights">
        <img src="global/assets/flight.svg" alt="" />
        <div class="summary__stats">
          <p><strong>12</strong> flights</p>
          <p><strong>8</strong> international</p>
          <p><strong>4</strong> domestic</p>
        </div>
      </div>
      <div class="summary__flights">
        <img src="global/assets/distance.svg" alt="" />
        <div class="summary__stats">
          <p><strong>5670</strong> km</p>
          <p><strong>0,14x</strong> around the earth</p>
          <p><strong>0,1x</strong> to the moon</p>
        </div>
      </div>
      <div class="summary__flights">
        <img src="global/assets/time.svg" alt="" />
        <div class="summary__stats">
          <p><strong>29h 35 min</strong></p>
          <p><strong>1.23</strong> days</p>
          <p><strong>0.4</strong> months</p>
        </div>
      </div>
    </div>`,
  styleUrl: './flights-summary.component.scss',
})
export class FlightsSummaryComponent {}
