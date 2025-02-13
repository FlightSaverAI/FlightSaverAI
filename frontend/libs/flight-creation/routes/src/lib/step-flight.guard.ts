import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { StepFlightComponent } from '@flight-saver/flight-creation/features';
import { AlertService } from '@shared/data-access';

export const stepFlightGuard: CanDeactivateFn<StepFlightComponent> = (
  component,
  currentRoute,
  currentState,
  nextState
) => {
  const _alertSevice = inject(AlertService);
  if (
    nextState.url.includes('flight-creation') &&
    component.flightDetailsForm.invalid &&
    component.aircraftDetailsForm.invalid
  ) {
    _alertSevice.showAlert('warning', 'Invalid forms');
    return false;
  }

  return true;
};
