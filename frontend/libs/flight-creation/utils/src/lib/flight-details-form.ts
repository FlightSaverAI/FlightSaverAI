import { inject } from "@angular/core";
import { NonNullableFormBuilder, Validators } from "@angular/forms";

export function flightDetailsForm() {
  return inject(NonNullableFormBuilder).group({
    departureDate: ['', Validators.required],
    departureAirport: ['', Validators.required],
    arrivalAirport: ['', Validators.required],
    flightNumber: ['', Validators.required],
    departureTimeHour: ['', Validators.required],
    departureTimeMinutes: ['', Validators.required],
    arrivalTimeHour: ['', Validators.required],
    arrivalTimeMinutes: ['', Validators.required],
    flightDuration: ['', Validators.required],
  });
}
