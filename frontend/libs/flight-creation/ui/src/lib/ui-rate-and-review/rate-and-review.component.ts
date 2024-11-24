import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextareaComponent, StarRatingComponent } from '@shared/ui-components';
import { NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'flight-creation-rate-and-review',
  standalone: true,
  imports: [
    CommonModule,
    TextareaComponent,
    StarRatingComponent,
    MatIconModule,
    ReactiveFormsModule,
  ],
  template: ` <form [formGroup]="form" class="cards">
    @for(item of rateAndReview; track item; let cardIndex = $index){
    <div class="card">
      <div class="card__image-container">
        <img class="card__image" [src]="item.imgSrc" alt="" />
        <div class="card__title">
          <p>Louisville / Louisville</p>
        </div>
      </div>
      <div class="card__opinion-container">
        <shared-textarea
          [formControlName]="item.formControlName || null"
          [parentForm]="form"
          [fieldName]="item.fieldName"
          placeholder="Share your opinion..."
        ></shared-textarea>
        <div class="star-rating">
          @for(star of item.stars; track star; let starIndex = $index){
          <shared-star-rating
            [state]="star"
            [starIndex]="starIndex"
            (selectRate)="selectRate($event, cardIndex)"
          ></shared-star-rating>
          }
        </div>
      </div>
    </div>
    }
  </form>`,
  styleUrl: './rate-and-review.component.scss',
})
export class RateAndReviewComponent {
  rateAndReview = [
    {
      formControlName: 'airportOpinion',
      fieldName: 'airportOpinion',
      imgSrc: 'global/assets/images/top-airports.jpg',
      stars: [false, false, false, false, false],
    },
    {
      formControlName: 'airlinesOpinion',
      fieldName: 'airlinesOpinion',
      imgSrc: 'global/assets/images/top-airports.jpg',
      stars: [false, false, false, false, false],
    },
    {
      formControlName: 'airlinesOpinion',
      fieldName: 'airlinesOpinion',
      imgSrc: 'global/assets/images/top-airlines.jpg',
      stars: [false, false, false, false, false],
    },
    {
      formControlName: 'airPlaneOpinion',
      fieldName: 'airPlaneOpinion',
      imgSrc: 'global/assets/images/activity-per-week.jpeg',
      stars: [false, false, false, false, false],
    },
  ];

  form = inject(NonNullableFormBuilder).group({
    airportOpinion: [],
    airlinesOpinion: [''],
    airPlaneOpinion: [''],
  });

  public selectRate(starIndex: number, cardIndex: number) {
    this.rateAndReview[cardIndex].stars = this.rateAndReview[cardIndex].stars.map(
      (_, index) => index <= starIndex
    );
  }
}
