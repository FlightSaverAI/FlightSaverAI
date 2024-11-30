import { CanDeactivateFn } from '@angular/router';
import { StepTicketComponent } from '@flight-saver/flight-creation/features';

export const stepTicketGuard: CanDeactivateFn<StepTicketComponent> = (
  component,
  currentRoute,
  currentState,
  nextState
) => {
  if (nextState.url.includes('flight-creation') && component.ticketForm.invalid) {
    alert('invalid form');
    return false;
  }
  return true;
};
