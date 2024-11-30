import { Component, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketFormComponent } from '@flight-saver/flight-creation/ui';
import { ticketForm } from '@flight-saver/flight-creation/utils';

type TicketForm = 'class' | 'seat' | 'reason';

@Component({
  standalone: true,
  imports: [CommonModule, TicketFormComponent],
  template: `<flight-creation-ticket-form
    [ticketForm]="ticketForm"
    [classOptions]="classOptions"
    [seatOptions]="seatOptions"
    [reasonOptions]="reasonOptions"
    (chooseOption)="chooseOption($event)"
  ></flight-creation-ticket-form>`,
})
export class StepTicketComponent {
  ticketForm = ticketForm();

  classOptions = ['Economy', 'Economy +', 'Business', 'First', 'Private'];
  seatOptions = ['Window', 'Middle', 'Aisle'];
  reasonOptions = ['Leisure', 'Business', 'Crew', 'Other'];

  queryParamsEffect = effect(
    () => {
      const currentFormState = JSON.parse(sessionStorage.getItem('formsState') || '{}');

      if (currentFormState.ticketForm) {
        this.ticketForm.patchValue({
          ...currentFormState.ticketForm,
        });
      }
    },
    { allowSignalWrites: true }
  );

  chooseOption({ control, option }: { control: TicketForm; option: string }) {
    this.ticketForm.controls[control].setValue(option);
  }
}
