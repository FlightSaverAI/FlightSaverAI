import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export type UserProfileUpdateForm = ReturnType<typeof userProfileUpdateForm>;

export function userProfileUpdateForm() {
  return inject(NonNullableFormBuilder).group({
    username: [''],
    email: ['', [Validators.email]],
  });
}
