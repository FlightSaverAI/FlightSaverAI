import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { provideStore } from '@ngrx/store';
import { AuthEffects, authReducer } from '@flight-saver/authentication/data-access';
import { provideEffects } from '@ngrx/effects';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    provideStore({ auth: authReducer.reducer }),
    provideEffects(AuthEffects),
  ]
};
