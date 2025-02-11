import { createActionGroup, props } from '@ngrx/store';
import { CurrentUser, LoginFormData } from '@flight-saver/authentication/models';
import { RegistrationFormData } from '@flight-saver/authentication/models';

export const authActions = createActionGroup({
  source: 'auth',
  events: {
    register: props<{ registerFormData: RegistrationFormData }>(),
    login: props<{ loginFormData: LoginFormData }>(),
    loginSuccess: props<{ currentUser: CurrentUser }>(),
    loginError: props<{ error: string }>(),
  },
});
