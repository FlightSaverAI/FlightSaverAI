import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export type LoginForm = ReturnType<typeof loginForm>;

export function loginForm() {
  return inject(NonNullableFormBuilder).group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });
}
