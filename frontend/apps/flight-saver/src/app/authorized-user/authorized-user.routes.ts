import { Route } from '@angular/router';
import { AuthorizedUserComponent } from './authorized-user.component';
import { loadRemoteModule } from '@angular-architects/native-federation';

export const authorizedUserRoutes: Route[] = [
  {
    path: '',
    component: AuthorizedUserComponent,
    children: [
      {
        path: 'home',
        loadChildren: async () =>
          await import('@flight-saver/home/routes').then((m) => m.homeRoutes),
      },
      {
        path: 'community',
        loadComponent: () =>
          loadRemoteModule('community', './Component').then((m) => m.AppComponent),
      },
    ],
  },
];
