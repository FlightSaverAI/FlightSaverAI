import { createActionGroup, props } from '@ngrx/store';
import { LoginFormData } from '@flight-saver/authentication/models';
import { RegistrationFormData } from '@flight-saver/authentication/models';

export const authActions = createActionGroup({
  source: 'auth',
  events: {
    register: props<{ registerFormData: RegistrationFormData }>(),
    login: props<{ loginFormData: LoginFormData }>(),
    loginSuccess: props<{ response: any }>(),
    loginError: props<{ error: string }>(),
  },
});
