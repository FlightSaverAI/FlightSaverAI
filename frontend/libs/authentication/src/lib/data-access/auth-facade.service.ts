import { inject, Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { LoginFormData } from '../models/login-form-data.interface';
import { loginActions } from './store/auth.actions';

@Injectable({
  providedIn: 'root',
})
export class AuthFacadeService {
  private _store = inject(Store);

  login(loginFormData: LoginFormData) {
    this._store.dispatch(loginActions.login(loginFormData));
  }
}
