import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { timer } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LoaderService } from './loader.service';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  const _loaderService = inject(LoaderService);

  const delayTimer = timer(100).subscribe(() => {
    _loaderService.show();
  });

  return next(req).pipe(
    finalize(() => {
      _loaderService.hide();
      delayTimer.unsubscribe();
    })
  );
};
