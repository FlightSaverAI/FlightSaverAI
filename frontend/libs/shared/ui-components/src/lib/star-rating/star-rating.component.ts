import { Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'shared-star-rating',
  standalone: true,
  imports: [CommonModule],
  template: `
    <span class="star" [ngClass]="{ filled: state() }" (click)="selectRate.emit(starIndex())"
      >★</span
    >
  `,
  styleUrl: './star-rating.component.scss',
})
export class StarRatingComponent {
  starIndex = input.required<number>();
  state = input.required<boolean>();
  selectRate = output<number>();
}
