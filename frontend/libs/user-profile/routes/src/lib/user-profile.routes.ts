import { Route } from '@angular/router';
import {
  SettingsComponent,
  UserProfileContainerComponent,
} from '@flight-saver/user-profile/features';

export const userProfileRoutes: Route[] = [
  {
    path: '',
    component: UserProfileContainerComponent,
  },
  {
    path: 'settings',
    component: SettingsComponent,
  },
];
