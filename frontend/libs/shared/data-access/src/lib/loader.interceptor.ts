import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { timer } from 'rxjs';
import { finalize, switchMap } from 'rxjs/operators';
import { LoaderService } from './loader.service';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  const _loaderService = inject(LoaderService);
  const showLoader = timer(10).pipe(
    switchMap(() => {
      _loaderService.show();
      return next(req);
    })
  );

  return showLoader.pipe(finalize(() => _loaderService.hide()));
};
