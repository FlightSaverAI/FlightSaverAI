import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export type CreatePostForm = ReturnType<typeof createPostForm>;

export function createPostForm() {
  return inject(NonNullableFormBuilder).group({
    Image: [''] as any,
    Content: ['', [Validators.required]],
    Country: [''],
    City: [''],
  });
}
