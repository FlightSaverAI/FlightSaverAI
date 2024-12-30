import { createFeature, createReducer, on } from '@ngrx/store';
import { authInitialState, AuthState } from '../state/auth.state';
import { authActions } from './auth.actions';

export const authReducer = createFeature({
  name: 'auth',
  reducer: createReducer<AuthState>(
    authInitialState,
    on(authActions.loginSuccess, (_, { response }) => ({
      currentUser: response,
    }))
  ),
});
