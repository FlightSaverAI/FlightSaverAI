import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'flight-creation-ticket-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonComponent],
  template: `
    <form [formGroup]="ticketForm()">
      <div>
        <h3>Class</h3>
        <div>
          @for(option of classOptions(); track option){
          <shared-button
            [content]="option"
            [category]="'secondary'"
            [isActive]="ticketForm().controls.class.value === option"
            (emitEvent)="chooseOption.emit({ control: 'class', option: option })"
          ></shared-button>
          }
        </div>
      </div>
      <div>
        <h3>Seat</h3>
        <div>
          @for(option of seatOptions(); track option){
          <shared-button
            [content]="option"
            [category]="'secondary'"
            [isActive]="ticketForm().controls.seat.value === option"
            (emitEvent)="chooseOption.emit({ control: 'seat', option: option })"
          ></shared-button>
          }
        </div>
      </div>
      <div>
        <h3>Reason</h3>
        <div>
          @for(option of reasonOptions(); track option){
          <shared-button
            [content]="option"
            [category]="'secondary'"
            [isActive]="ticketForm().controls.reason.value === option"
            (emitEvent)="chooseOption.emit({ control: 'reason', option: option })"
          ></shared-button>
          }
        </div>
      </div>
    </form>
  `,
  styleUrl: './ticket-form.component.scss',
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketFormComponent {
  ticketForm = input.required<any>();

  classOptions = input.required<any[]>();
  seatOptions = input.required<any[]>();
  reasonOptions = input.required<any[]>();

  chooseOption = output<any>();
}
