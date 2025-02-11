import { Route } from '@angular/router';
import { AuthorizedUserComponent } from './authorized-user.component';
import { loadRemoteModule } from '@angular-architects/native-federation';
import { PageNotFoundComponent } from '@shared/ui';
import { authorizedUserGuard } from './authorized-user.guard';

export const authorizedUserRoutes: Route[] = [
  {
    path: '',
    component: AuthorizedUserComponent,
    canActivate: [authorizedUserGuard],
    children: [
      {
        path: 'admin-panel',
        loadComponent: () =>
          loadRemoteModule('admin-panel', './Component').then((m) => m.AppComponent),
      },
      {
        path: 'home',
        loadChildren: async () =>
          await import('@flight-saver/home/routes').then((m) => m.homeRoutes),
      },
      {
        path: 'statistics',
        loadChildren: async () =>
          await import('@flight-saver/statistics/routes').then((m) => m.statisticsRoutes),
      },
      {
        path: 'community',
        loadComponent: () =>
          loadRemoteModule('community', './Component').then((m) => m.AppComponent),
      },
      {
        path: 'flight-creation',
        loadChildren: async () =>
          await import('@flight-saver/flight-creation/routes').then((m) => m.flightCreationRoutes),
      },
      {
        path: 'user-profile',
        loadChildren: async () =>
          await import('@flight-saver/user-profile/routes').then((m) => m.userProfileRoutes),
      },
      {
        path: 'users-search',
        loadChildren: async () =>
          await import('@flight-saver/friends/routes').then((m) => m.friendsRoutes),
      },

      { path: '**', component: PageNotFoundComponent },
    ],
  },
];
