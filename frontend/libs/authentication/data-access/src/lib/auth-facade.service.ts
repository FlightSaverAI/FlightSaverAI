import { inject, Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { LoginFormData, RegistrationFormData } from '@flight-saver/authentication/models';
import { authActions } from './store/auth.actions';
import { AuthState } from './state/auth.state';

@Injectable({
  providedIn: 'root',
})
export class AuthFacadeService {
  private _store = inject(Store<AuthState>);

  public login(loginFormData: LoginFormData) {
    this._store.dispatch(authActions.login({ loginFormData }));
  }

  public registration(registerFormData: RegistrationFormData) {
    this._store.dispatch(authActions.register({ registerFormData }));
  }
}
