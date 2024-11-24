import { CanDeactivateFn } from '@angular/router';
import { FlightFormComponent } from '@flight-saver/flight-creation/ui';

export const flightFormGuard: CanDeactivateFn<FlightFormComponent> = (
  component,
  currentRoute,
  currentState,
  nextState
) => {
  console.log(nextState);

  if (
    nextState.url.includes('flight-creation') &&
    component.flightDetailsForm.invalid &&
    component.aircraftDetailsForm.invalid
  ) {
    alert('Invalid forms');
    return false;
  }

  return true;
};
