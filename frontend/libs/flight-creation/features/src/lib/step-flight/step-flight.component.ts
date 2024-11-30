import { Component, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightFormComponent } from '@flight-saver/flight-creation/ui';
import { aircraftDetailsForm, flightDetailsForm } from '@flight-saver/flight-creation/utils';

@Component({
  standalone: true,
  imports: [CommonModule, FlightFormComponent],
  template: `<flight-creation-flight-form
    [flightDetailsForm]="flightDetailsForm"
    [aircraftDetailsForm]="aircraftDetailsForm"
  ></flight-creation-flight-form>`,
})
export class StepFlightComponent {
  public flightDetailsForm = flightDetailsForm();
  public aircraftDetailsForm = aircraftDetailsForm();

  queryParamsEffect = effect(
    () => {
      const currentFormState = JSON.parse(sessionStorage.getItem('formsState') || '{}');

      if (currentFormState.flightDetailsForm) {
        this.flightDetailsForm.patchValue({
          ...currentFormState.flightDetailsForm,
        });
      }

      if (currentFormState.aircraftDetailsForm) {
        this.aircraftDetailsForm.patchValue({
          ...currentFormState.aircraftDetailsForm,
        });
      }
    },
    { allowSignalWrites: true }
  );
}
