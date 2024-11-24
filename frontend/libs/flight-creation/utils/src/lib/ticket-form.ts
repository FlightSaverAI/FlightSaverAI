import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export function ticketForm() {
  return inject(NonNullableFormBuilder).group({
    class: ['', Validators.required],
    seat: ['', Validators.required],
    reason: ['', Validators.required],
  });
}
