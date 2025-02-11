import { Route } from '@angular/router';
import { FriendProfileComponent, FriendsSearchComponent } from '@flight-saver/friends/features';

export const friendsRoutes: Route[] = [
  {
    path: '',
    component: FriendsSearchComponent,
  },
  {
    path: ':id',
    component: FriendProfileComponent,
  },
];
