import { createActionGroup, props } from '@ngrx/store';
import { LoginFormData } from '@flight-saver/authentication/models';
import { RegistrationFormData } from '@flight-saver/authentication/models';
import { CurrentUser } from '@flight-saver/authentication/models';

export const loginActions = createActionGroup({
  source: 'login',
  events: {
    login: props<{ loginFormData: LoginFormData }>(),
    loginSuccess: props<{ currentUser: CurrentUser }>(),
  },
});

export const registerActions = createActionGroup({
  source: 'register',
  events: {
    register: props<{ registerFormData: RegistrationFormData }>(),
    registerSuccess: props<{ currentUser: CurrentUser }>(),
  },
});
