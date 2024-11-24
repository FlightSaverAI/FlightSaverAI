import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export function aircraftDetailsForm() {
  return inject(NonNullableFormBuilder).group({
    airline: ['', Validators.required],
    aircraftType: ['', Validators.required],
    aircraftReg: ['', Validators.required],
  });
}
