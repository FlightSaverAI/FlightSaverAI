import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const flightCreationGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const urlTree = router.parseUrl(state.url);
  const queryParams = { ...urlTree.queryParams };

  if (!queryParams['stepNumber']) {
    queryParams['stepNumber'] = 1;

    return router.createUrlTree(['/authorized/flight-creation/flight'], {
      queryParams: queryParams,
    });
  }

  return true;
};
