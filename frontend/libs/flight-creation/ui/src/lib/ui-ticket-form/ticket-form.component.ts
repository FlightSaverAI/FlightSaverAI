import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonComponent } from '@shared/ui-components';
import { ticketForm } from '@flight-saver/flight-creation/utils';

type TicketForm = 'class' | 'seat' | 'reason';

@Component({
  selector: 'flight-creation-ticket-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonComponent],
  template: `
    <form [formGroup]="ticketForm">
      <div>
        <h3>Class</h3>
        <div>
          @for(option of classOptions; track option){
          <shared-button
            [content]="option"
            [category]="'secondary'"
            [isActive]="ticketForm.controls.class.value === option"
            (emitEvent)="chooseOption('class', option)"
          ></shared-button>
          }
        </div>
      </div>
      <div>
        <h3>Seat</h3>
        <div>
          @for(option of seatOptions; track option){
          <shared-button
            [content]="option"
            [category]="'secondary'"
            [isActive]="ticketForm.controls.seat.value === option"
            (emitEvent)="chooseOption('seat', option)"
          ></shared-button>
          }
        </div>
      </div>
      <div>
        <h3>Reason</h3>
        <div>
          @for(option of reasonOptions; track option){
          <shared-button
            [content]="option"
            [category]="'secondary'"
            [isActive]="ticketForm.controls.reason.value === option"
            (emitEvent)="chooseOption('reason', option)"
          ></shared-button>
          }
        </div>
      </div>
    </form>
  `,
  styleUrl: './ticket-form.component.scss',
})
export class TicketFormComponent {
  classOptions = ['Economy', 'Economy +', 'Business', 'First', 'Private'];
  seatOptions = ['Window', 'Middle', 'Aisle'];
  reasonOptions = ['Leisure', 'Business', 'Crew', 'Other'];

  ticketForm = ticketForm();

  chooseOption(control: TicketForm, option: string) {
    this.ticketForm.controls[control].setValue(option);
  }
}
