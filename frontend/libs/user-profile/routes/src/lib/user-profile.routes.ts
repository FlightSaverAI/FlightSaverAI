import { Route } from '@angular/router';
import { UserProfileContainerComponent } from '@flight-saver/user-profile/features';

export const userProfileRoutes: Route[] = [
  {
    path: '',
    component: UserProfileContainerComponent,
  },
];
