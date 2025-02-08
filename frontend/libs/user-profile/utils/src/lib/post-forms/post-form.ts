import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export type PostForm = ReturnType<typeof postForm>;

export function postForm() {
  return inject(NonNullableFormBuilder).group({
    image: [''] as any,
    content: ['', [Validators.required]],
    country: [''],
    city: [''],
  });
}
