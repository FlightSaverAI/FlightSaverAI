import { Route } from '@angular/router';
import { LoginContainerComponent } from '@flight-saver/authentication/features';
import { RegistrationContainerComponent } from '@flight-saver/authentication/features';

export const authRoutes: Route[] = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginContainerComponent,
  },
  {
    path: 'registration',
    component: RegistrationContainerComponent,
  },
];
