import { CurrentUser } from '../../models/current-user.interface';

export interface AuthState {
  currentUser: CurrentUser;
}

export const authInitialState = {
  currentUser: {} as CurrentUser,
};
