import { createFeature, createReducer, on } from '@ngrx/store';
import { authInitialState, AuthState } from '../state/auth.state';
import { loginActions, registerActions } from './auth.actions';

export const authReducer = createFeature({
  name: 'auth',
  reducer: createReducer<AuthState>(
    authInitialState,
    on(loginActions.loginSuccess, registerActions.registerSuccess, (_, { currentUser }) => ({
      currentUser: currentUser,
    }))
  ),
});
