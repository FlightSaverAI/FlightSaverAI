import { Route } from '@angular/router';
import { FlightCreationContainerComponent } from '@flight-saver/flight-creation/features';

export const flightCreationRoutes: Route[] = [
  {
    path: '',
    component: FlightCreationContainerComponent,
  },
];
