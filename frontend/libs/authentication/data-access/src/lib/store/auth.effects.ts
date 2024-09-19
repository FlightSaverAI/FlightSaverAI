import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { loginActions, registerActions } from './auth.actions';
import { map, switchMap, tap } from 'rxjs';
import { AuthService } from '../auth.service';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthEffects {
  private _actions = inject(Actions);
  private _authService = inject(AuthService);
  private _cookieService = inject(CookieService);

  login$ = createEffect(() =>
    this._actions.pipe(
      ofType(loginActions.login),
      switchMap(({ loginFormData }) => this._authService.authentication(loginFormData)),
      tap(({ token }) => this._cookieService.set('AuthToken', token)),
      map(({ token, ...currentUser }) => loginActions.loginSuccess({ currentUser }))
    )
  );

  register$ = createEffect(() =>
    this._actions.pipe(
      ofType(registerActions.register),
      switchMap(({ registerFormData }) => this._authService.authentication(registerFormData)),
      tap(({ token }) => this._cookieService.set('AuthToken', token)),
      map(({ token, ...currentUser }) => registerActions.registerSuccess({ currentUser }))
    )
  );
}
