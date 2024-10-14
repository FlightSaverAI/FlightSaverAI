import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { loginActions, registerActions } from './auth.actions';
import { map, switchMap, tap } from 'rxjs';
import { AuthService } from '../auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable()
export class AuthEffects {
  private _actions = inject(Actions);
  private _authService = inject(AuthService);
  private _cookieService = inject(CookieService);
  private _router = inject(Router);

  login$ = createEffect(() =>
    this._actions.pipe(
      ofType(loginActions.login),
      switchMap(({ loginFormData }) => this._authService.authentication(loginFormData)),
      tap(({ token }) => this._cookieService.set('AuthToken', token)),
      map(({ token, ...currentUser }) => loginActions.loginSuccess({ currentUser })),
      tap(() => this._router.navigateByUrl('/authorized/home'))
    )
  );

  register$ = createEffect(() =>
    this._actions.pipe(
      ofType(registerActions.register),
      switchMap(({ registerFormData }) => this._authService.authentication(registerFormData)),
      tap(({ token }) => this._cookieService.set('AuthToken', token)),
      map(({ token, ...currentUser }) => registerActions.registerSuccess({ currentUser })),
      tap(() => this._router.navigateByUrl('/authorized/home'))
    )
  );
}
