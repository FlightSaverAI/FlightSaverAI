import { ChangeDetectionStrategy, Component, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightFormComponent } from '@flight-saver/flight-creation/ui';
import { aircraftDetailsForm, flightDetailsForm } from '@flight-saver/flight-creation/utils';
import { FlightFormService } from '@flight-saver/flight-creation/data-access';

@Component({
  standalone: true,
  imports: [CommonModule, FlightFormComponent],
  template: `<flight-creation-flight-form
    [flightDetailsForm]="flightDetailsForm"
    [aircraftDetailsForm]="aircraftDetailsForm"
    [initializeFlightFormOptions]="initializeFlightFormOptions()"
  ></flight-creation-flight-form>`,
  providers: [FlightFormService],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StepFlightComponent {
  public flightDetailsForm = flightDetailsForm();
  public aircraftDetailsForm = aircraftDetailsForm();

  private _flightFormService = inject(FlightFormService);

  protected initializeFlightFormOptions = signal({
    hours: [...Array(24).keys()].map((n) => (n <= 9 ? `0${n}` : n.toString())),
    minutes: [...Array(60).keys()].map((n) => (n < 9 ? `0${n}` : n.toString())),
    airports: this._flightFormService.getAirports(),
    airlines: this._flightFormService.getAirlines(),
    aircrafts: this._flightFormService.getAircrafts(),
  });

  protected queryParamsEffect = effect(
    () => {
      const currentFormState = JSON.parse(sessionStorage.getItem('formsState') || '{}');

      if (currentFormState.flightDetailsForm) {
        const { departureAirport, arrivalAirport, ...rest } = currentFormState.flightDetailsForm;
        this.flightDetailsForm.patchValue({
          departureAirport,
          arrivalAirport,
          ...rest,
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
