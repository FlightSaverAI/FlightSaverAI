import { Route } from '@angular/router';
import { FlightCreationContainerComponent } from '@flight-saver/flight-creation/features';
import { FlightFormComponent, TicketFormComponent } from '@flight-saver/flight-creation/ui';
import { flightFormGuard } from './flight-form.guard';
import { ticketFormGuard } from './ticket-form.guard';
import { flightCreationGuard } from './flight-creation.guard';

export const flightCreationRoutes: Route[] = [
  {
    path: '',
    component: FlightCreationContainerComponent,
    canActivate: [flightCreationGuard],
    children: [
      {
        path: 'flight',
        component: FlightFormComponent,
        canDeactivate: [flightFormGuard],
      },
      {
        path: 'ticket',
        component: TicketFormComponent,
        canDeactivate: [ticketFormGuard],
      },
      {
        path: 'rate-and-review',
        component: FlightFormComponent,
      },
    ],
  },
];
