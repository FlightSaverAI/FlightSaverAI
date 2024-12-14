import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextareaComponent, StarRatingComponent } from '@shared/ui-components';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'flight-creation-rate-and-review',
  standalone: true,
  imports: [CommonModule, TextareaComponent, StarRatingComponent, ReactiveFormsModule],
  template: `
    <div class="cards">
      @for(item of rateAndReviewFormConfig(); track item){
      <form [formGroup]="rateAndReviewForm().get(item.fieldName)">
        <div class="card">
          <div class="card__image-container">
            <img class="card__image" [src]="item.imgSrc" alt="" />
            <div class="card__title">
              <p>Louisville / Louisville</p>
            </div>
          </div>
          <div class="card__opinion-container">
            <shared-textarea
              formControlName="review"
              [parentForm]="rateAndReviewForm().get(item.fieldName)"
              [fieldName]="'review'"
              placeholder="Share your opinion..."
            ></shared-textarea>
            <div class="star-rating">
              @for(star of getStars(item.fieldName); track star; let starIndex = $index){
              <shared-star-rating
                [state]="star"
                [starIndex]="starIndex"
                (selectRate)="emitSelectRate($event, item.fieldName)"
              ></shared-star-rating>
              }
            </div>
          </div>
        </div>
      </form>
      }
    </div>
  `,
  styleUrl: './rate-and-review.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RateAndReviewComponent {
  rateAndReviewForm = input.required<any>();
  rateAndReviewFormConfig = input.required<any>();

  selectRate = output<any>();

  public getStars(fieldName: string): boolean[] {
    return this.rateAndReviewForm().get(fieldName).value.stars;
  }

  public emitSelectRate(starIndex: number, fieldName: string) {
    this.selectRate.emit({ starIndex, fieldName });
  }
}
