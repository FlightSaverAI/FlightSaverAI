import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { StepTicketComponent } from '@flight-saver/flight-creation/features';
import { AlertService } from '@shared/data-access';

export const stepTicketGuard: CanDeactivateFn<StepTicketComponent> = (
  component,
  currentRoute,
  currentState,
  nextState
) => {
  const _alertSevice = inject(AlertService);

  if (nextState.url.includes('flight-creation') && component.ticketForm.invalid) {
    _alertSevice.showAlert('warning', 'Invalid form');
    return false;
  }
  return true;
};
