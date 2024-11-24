import { CanDeactivateFn } from '@angular/router';
import { TicketFormComponent } from '@flight-saver/flight-creation/ui';

export const ticketFormGuard: CanDeactivateFn<TicketFormComponent> = (
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
