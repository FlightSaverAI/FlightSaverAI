import * as jwt_decode from 'jwt-decode';
import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { authActions } from './auth.actions';
import { catchError, map, Observable, of, switchMap, tap } from 'rxjs';
import { AuthService } from '../auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AlertService } from '@shared/data-access';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class AuthEffects {
  private _actions = inject(Actions);
  private _authService = inject(AuthService);
  private _cookieService = inject(CookieService);
  private _router = inject(Router);
  private _alertService = inject(AlertService);
  private _homeUrl = '/authorized/home';

  register$ = createEffect(() =>
    this._actions.pipe(
      ofType(authActions.register),
      switchMap(({ registerFormData }) =>
        this._processAuthentication(this._authService.registration(registerFormData))
      )
    )
  );

  login$ = createEffect(() =>
    this._actions.pipe(
      ofType(authActions.login),
      switchMap(({ loginFormData }) =>
        this._processAuthentication(this._authService.authentication(loginFormData))
      )
    )
  );

  loginSuccessNavigate$ = createEffect(
    () =>
      this._actions.pipe(
        ofType(authActions.loginSuccess),
        tap(() => this._router.navigateByUrl(this._homeUrl))
      ),
    { dispatch: false }
  );

  private _processAuthentication(apiCall$: Observable<any>) {
    return apiCall$.pipe(
      tap(({ token }) => this._cookieService.set('AuthToken', token)),
      map(({ token }) => jwt_decode.jwtDecode(token)),
      map((response: any) => authActions.loginSuccess({ response })),
      catchError(({ error }: HttpErrorResponse) => {
        this._alertService.showAlert('error', error.details);
        return of(authActions.loginError({ error: error || 'Unknown error' }));
      })
    );
  }
}
