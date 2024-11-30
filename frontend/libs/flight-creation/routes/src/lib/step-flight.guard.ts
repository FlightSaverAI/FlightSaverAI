import { CanDeactivateFn } from '@angular/router';
import { StepFlightComponent } from '@flight-saver/flight-creation/features';

export const stepFlightGuard: CanDeactivateFn<StepFlightComponent> = (
  component,
  currentRoute,
  currentState,
  nextState
) => {
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
