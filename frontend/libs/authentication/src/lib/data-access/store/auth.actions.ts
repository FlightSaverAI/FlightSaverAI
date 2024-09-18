import { createActionGroup, props } from '@ngrx/store';
import { LoginFormData } from '../../models/login-form-data.interface';
import { RegistrationFormData } from '../../models/registration-form-data.interface';
import { CurrentUser } from '../../models/current-user.interface';

export const loginActions = createActionGroup({
  source: 'login',
  events: {
    login: props<LoginFormData>(),
    loginSuccess: props<{ currentUser: CurrentUser }>(),
  },
});

export const registerActions = createActionGroup({
  source: 'register',
  events: {
    register: props<RegistrationFormData>(),
    registerSuccess: props<{ currentUser: CurrentUser }>(),
  },
});
