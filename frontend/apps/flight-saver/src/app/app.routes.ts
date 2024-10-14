import { Route } from '@angular/router';

export const appRoutes: Route[] = [
  {
    path: '',
    loadChildren: async () =>
      await import('@flight-saver/authentication/routes').then((m) => m.authRoutes),
  },
  {
    path: 'authorized',
    loadChildren: () =>
      import('./authorized-user/authorized-user.routes').then((m) => m.authorizedUserRoutes),
  },
];
