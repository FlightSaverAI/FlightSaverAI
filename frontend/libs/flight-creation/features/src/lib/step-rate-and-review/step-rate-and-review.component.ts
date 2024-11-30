import { Component, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RateAndReviewComponent } from '@flight-saver/flight-creation/ui';
import { rateAndReviewForm } from '@flight-saver/flight-creation/utils';

@Component({
  standalone: true,
  imports: [CommonModule, RateAndReviewComponent],
  template: `<flight-creation-rate-and-review
    [rateAndReviewForm]="rateAndReviewForm"
    [rateAndReviewFormConfig]="rateAndReviewFormConfig"
    (selectRate)="selectRate($event)"
  ></flight-creation-rate-and-review>`,
})
export class StepRateAndReviewComponent {
  rateAndReviewForm = rateAndReviewForm();

  rateAndReviewFormConfig = [
    {
      fieldName: 'airportOpinion',
      imgSrc: 'global/assets/images/top-airports.jpg',
    },
    {
      fieldName: 'airlinesOpinion',
      imgSrc: 'global/assets/images/top-airports.jpg',
    },
    {
      fieldName: 'airlinesOpinion',
      imgSrc: 'global/assets/images/top-airlines.jpg',
    },
    {
      fieldName: 'airPlaneOpinion',
      imgSrc: 'global/assets/images/activity-per-week.jpeg',
    },
  ];

  queryParamsEffect = effect(
    () => {
      const currentFormState = JSON.parse(sessionStorage.getItem('formsState') || '{}');

      if (currentFormState.rateAndReviewForm) {
        this.rateAndReviewForm.patchValue({
          ...currentFormState.rateAndReviewForm,
        });

        console.log(this.rateAndReviewForm);
      }
    },
    { allowSignalWrites: true }
  );

  public selectRate({ starIndex, fieldName }: any) {
    const control = this.rateAndReviewForm.get(fieldName);

    if (!control) {
      return;
    }

    const updatedStars = control.value.stars.map((_: any, index: number) => index <= starIndex);

    control.patchValue({
      stars: updatedStars,
      rate: updatedStars.filter(Boolean).length,
    });
  }
}
