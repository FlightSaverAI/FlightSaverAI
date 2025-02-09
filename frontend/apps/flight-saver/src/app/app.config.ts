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
import { loaderInterceptor } from '@shared/data-access';
import { provideAnimations } from '@angular/platform-browser/animations';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    provideStore({ auth: authReducer.reducer }),
    provideEffects(AuthEffects),
    provideHttpClient(withInterceptors([authInterceptor, loaderInterceptor])),
    provideAnimations()
  ],
};
