import { Route } from '@angular/router';
import { LoginContainerComponent } from '../features/feature-login/login-container.component';

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
];
