import { Route } from '@angular/router';

export const appRoutes: Route[] = [
  {
    path: '',
    loadChildren: async () =>
      await import('@flight-saver/authentication/routes').then((m) => m.authRoutes),
  },
];
