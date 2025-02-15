import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { passwordMatchValidator } from '@shared/utils';

export type ChangePasswordForm = ReturnType<typeof changePasswordForm>;

export function changePasswordForm() {
  return inject(NonNullableFormBuilder).group(
    {
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    },
    {
      validators: passwordMatchValidator('password', 'confirmPassword'),
    }
  );
}
