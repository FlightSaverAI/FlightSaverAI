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
import { CurrentUser } from '@flight-saver/authentication/models';

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

  private _processAuthentication(apiCall$: Observable<{ token: string }>) {
    return apiCall$.pipe(
      tap(({ token }) =>
        this._cookieService.set('AuthToken', token, { path: '/', sameSite: 'Lax' })
      ),
      map(({ token }) => {
        const decodedToken = jwt_decode.jwtDecode(token) as jwt_decode.JwtPayload & CurrentUser;
        return <CurrentUser>{
          id: decodedToken.id,
          name: decodedToken.name,
          email: decodedToken.email,
          role: decodedToken.role,
        };
      }),
      map((currentUser) => authActions.loginSuccess({ currentUser })),
      catchError(({ error }: HttpErrorResponse) => {
        this._alertService.showAlert('error', error.details);
        return of(authActions.loginError({ error: error || 'Unknown error' }));
      })
    );
  }
}
