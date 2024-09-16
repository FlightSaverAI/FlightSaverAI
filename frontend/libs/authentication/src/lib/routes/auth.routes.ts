import { Route } from '@angular/router';
import { LoginContainerComponent } from '../features/feature-login/login-container.component';
import { RegistrationContainerComponent } from '../features/feature-registration/registration-container.component';

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
