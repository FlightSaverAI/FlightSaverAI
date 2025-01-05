import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

export const authorizedUserGuard: CanActivateFn = () =>
  inject(CookieService).get('AuthToken') ? true : false;
