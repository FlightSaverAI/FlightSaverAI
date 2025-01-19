import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { provideStore } from '@ngrx/store';
import {
  AuthEffects,
  authReducer,
  authInterceptor,
} from '@flight-saver/authentication/data-access';
import { provideEffects } from '@ngrx/effects';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    provideStore({ auth: authReducer.reducer }),
    provideEffects(AuthEffects),
    provideHttpClient(withInterceptors([authInterceptor])),
  ],
};
