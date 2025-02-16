import { ChangeDetectionStrategy, Component, effect, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RateAndReviewComponent } from '@flight-saver/flight-creation/ui';
import { rateAndReviewForm } from '@flight-saver/flight-creation/utils';

@Component({
  standalone: true,
  imports: [CommonModule, RateAndReviewComponent],
  template: `<flight-creation-rate-and-review
    [rateAndReviewForm]="rateAndReviewForm()"
    [rateAndReviewFormConfig]="rateAndReviewFormConfig()"
    (selectRate)="selectRate($event)"
  ></flight-creation-rate-and-review>`,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StepRateAndReviewComponent {
  public rateAndReviewForm = signal(rateAndReviewForm());
  protected rateAndReviewFormConfig = signal([
    {
      fieldName: 'departureAirportOpinion',
      imgSrc: 'global/assets/images/top-airports.jpg',
      name: '',
    },
    {
      fieldName: 'arrivalAirportOpinion',
      imgSrc: 'global/assets/images/top-airports.jpg',
      name: '',
    },
    {
      fieldName: 'airlinesOpinion',
      imgSrc: 'global/assets/images/top-airlines.jpg',
      name: '',
    },
    {
      fieldName: 'airPlaneOpinion',
      imgSrc: 'global/assets/images/activity-per-week.jpeg',
      name: '',
    },
  ]);

  protected queryParamsEffect = effect(
    () => {
      const currentFormState = JSON.parse(sessionStorage.getItem('formsState') || '{}');

      this.rateAndReviewFormConfig.update((state) =>
        state.map((element) => {
          if (element.fieldName === 'departureAirportOpinion') {
            return { ...element, name: currentFormState.flightDetailsForm.departureAirport.name };
          } else if (element.fieldName === 'arrivalAirportOpinion') {
            return { ...element, name: currentFormState.flightDetailsForm.arrivalAirport.name };
          } else if (element.fieldName === 'airlinesOpinion') {
            return { ...element, name: currentFormState.aircraftDetailsForm.airline.name };
          } else {
            return { ...element, name: currentFormState.aircraftDetailsForm.aircraftType.name };
          }
        })
      );

      if (currentFormState.rateAndReviewForm) {
        this.rateAndReviewForm().patchValue({
          ...currentFormState.rateAndReviewForm,
        });
      }
    },
    { allowSignalWrites: true }
  );

  protected selectRate({ starIndex, fieldName }: any) {
    const control = this.rateAndReviewForm().get(fieldName);

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
