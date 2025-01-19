import { inject } from '@angular/core';
import { NonNullableFormBuilder } from '@angular/forms';

export function rateAndReviewForm() {
  return inject(NonNullableFormBuilder).group({
    departureAirportOpinion: createOpinionGroup(),
    arrivalAirportOpinion: createOpinionGroup(),
    airlinesOpinion: createOpinionGroup(),
    airPlaneOpinion: createOpinionGroup(),
  });
}

function createOpinionGroup() {
  return inject(NonNullableFormBuilder).group({
    rate: [0],
    review: [''],
    stars: [[false, false, false, false, false]],
  });
}
