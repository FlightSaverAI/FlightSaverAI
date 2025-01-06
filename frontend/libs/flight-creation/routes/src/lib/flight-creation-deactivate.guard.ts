import { CanDeactivateFn } from '@angular/router';

export const flightCreationDeactivateGuard: CanDeactivateFn<unknown> = (
  component,
  currentRoute,
  currentState,
  nextState
) => {
  if (!nextState.url.includes('flight-creation')) {
    sessionStorage.removeItem('formsState');
  }

  return true;
};
