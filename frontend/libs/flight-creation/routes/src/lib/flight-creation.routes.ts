import { Route } from '@angular/router';
import {
  FlightCreationContainerComponent,
  StepFlightComponent,
  StepRateAndReviewComponent,
  StepTicketComponent,
} from '@flight-saver/flight-creation/features';
import { stepFlightGuard } from './step-flight.guard';
import { stepTicketGuard } from './step-ticket.guard';
import { flightCreationGuard } from './flight-creation.guard';
import { flightCreationDeactivateGuard } from './flight-creation-deactivate.guard';

export const flightCreationRoutes: Route[] = [
  {
    path: '',
    component: FlightCreationContainerComponent,
    canActivate: [flightCreationGuard],
    canDeactivate: [flightCreationDeactivateGuard],
    children: [
      {
        path: 'flight',
        component: StepFlightComponent,
        canDeactivate: [stepFlightGuard],
      },
      {
        path: 'ticket',
        component: StepTicketComponent,
        canDeactivate: [stepTicketGuard],
      },
      {
        path: 'rate-and-review',
        component: StepRateAndReviewComponent,
      },
    ],
  },
];
