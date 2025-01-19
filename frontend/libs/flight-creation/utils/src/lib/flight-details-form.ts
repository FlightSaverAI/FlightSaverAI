import { inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

export function flightDetailsForm() {
  return inject(NonNullableFormBuilder).group({
    departureDate: ['', Validators.required],
    departureAirport: [{ id: '', name: '' }, Validators.required],
    arrivalAirport: [{ id: '', name: '' }, Validators.required],
    flightNumber: ['', Validators.required],
    departureTimeHour: ['', Validators.required],
    departureTimeMinutes: ['', Validators.required],
    arrivalTimeHour: ['', Validators.required],
    arrivalTimeMinutes: ['', Validators.required],
    flightDurationHour: ['', Validators.required],
    flightDurationMinutes: ['', Validators.required],
  });
}
