import { CurrentUser } from '@flight-saver/authentication/models';

export interface AuthState {
  currentUser: CurrentUser;
}

export const authInitialState = {
  currentUser: {} as CurrentUser,
};
