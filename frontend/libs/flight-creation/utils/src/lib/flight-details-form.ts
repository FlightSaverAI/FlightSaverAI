import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export function flightDetailsForm() {
  return inject(NonNullableFormBuilder).group({
    departureDate: ['', Validators.required],
    departureAirport: [{ id: '', name: '' }, Validators.required],
    arrivalAirport: [{ id: '', name: '' }, Validators.required],
    flightNumber: [''],
    departureTimeHour: [''],
    departureTimeMinutes: [''],
    arrivalTimeHour: [''],
    arrivalTimeMinutes: [''],
    flightDurationHour: [''],
    flightDurationMinutes: [''],
  });
}
