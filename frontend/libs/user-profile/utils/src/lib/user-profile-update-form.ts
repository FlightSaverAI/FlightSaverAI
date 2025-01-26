import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
//TO_FIX
import { passwordMatchValidator } from '@flight-saver/authentication/utils';

export type RegistrationForm = ReturnType<typeof userProfileUpdateForm>;

export function userProfileUpdateForm() {
  return inject(NonNullableFormBuilder).group(
    {
      username: [''],
      email: ['', [Validators.email]],
      password: [''],
      confirmPassword: ['', Validators.required],
    },
    {
      validators: passwordMatchValidator('password', 'confirmPassword'),
    }
  );
}
