import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { passwordMatchValidator } from './password-match-validator';

export type RegistrationForm = ReturnType<typeof registrationForm>;

export function registrationForm() {
  return inject(NonNullableFormBuilder).group(
    {
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    },
    {
      validators: passwordMatchValidator('password', 'confirmPassword'),
    }
  );
}
