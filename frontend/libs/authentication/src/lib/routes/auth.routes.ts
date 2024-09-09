import { Route } from '@angular/router';
import { LoginComponent } from '../features/feature-login/login-container.component';

export const authRoutes: Route[] = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
];
