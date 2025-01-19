import {
  ChangeDetectionStrategy,
  Component,
  input,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from '@shared/ui-components';
import { SelectComponent } from '@shared/ui-components';
import { DatePickerComponent } from '@shared/ui-components';

@Component({
  selector: 'flight-creation-flight-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputComponent,
    SelectComponent,
    DatePickerComponent,
  ],
  template: `<div class="wrapper">
    <form [formGroup]="flightDetailsForm()">
      <div class="part1">
        <shared-date-picker
          formControlName="departureDate"
          [parentForm]="flightDetailsForm()"
          fieldName="departureDate"
          label="Departure Date"
          placeholder="Departure Date"
        ></shared-date-picker>
        <shared-select
          formControlName="departureAirport"
          [parentForm]="flightDetailsForm()"
          [options]="initializeFlightFormOptions().airports"
          fieldName="departureAirport"
          label="Departure Airport"
          placeholder="Departure Airport"
        ></shared-select>
        <shared-select
          formControlName="arrivalAirport"
          [parentForm]="flightDetailsForm()"
          [options]="initializeFlightFormOptions().airports"
          fieldName="arrivalAirport"
          label="Arrival Airport"
          placeholder="Arrival Airport"
        ></shared-select>
      </div>
      <div class="part2">
        <shared-input
          formControlName="flightNumber"
          [parentForm]="flightDetailsForm()"
          fieldName="flightNumber"
          label="Flight Number"
          placeholder="Flight Number"
        ></shared-input>
        <div class="check">
          <shared-select
            class="time"
            formControlName="departureTimeHour"
            [parentForm]="flightDetailsForm()"
            [options]="initializeFlightFormOptions().hours"
            fieldName="departureTimeHour"
            label="Departure Time"
            placeholder="Hour"
          ></shared-select>
          <span class="colon">:</span>
          <shared-select
            class="time"
            formControlName="departureTimeMinutes"
            [parentForm]="flightDetailsForm()"
            [options]="initializeFlightFormOptions().minutes"
            fieldName="departureTimeMinutes"
            placeholder="Minutes"
          ></shared-select>
        </div>
        <div class="check">
          <shared-select
            class="time"
            formControlName="arrivalTimeHour"
            [parentForm]="flightDetailsForm()"
            [options]="initializeFlightFormOptions().hours"
            fieldName="arrivalTimeHour"
            label="Arrival Time"
            placeholder="Hour"
          ></shared-select>
          <span class="colon">:</span>
          <shared-select
            class="time"
            formControlName="arrivalTimeMinutes"
            [parentForm]="flightDetailsForm()"
            [options]="initializeFlightFormOptions().minutes"
            fieldName="arrivalTimeMinutes"
            placeholder="Minutes"
          ></shared-select>
        </div>
        <div class="check">
          <shared-select
            class="time"
            formControlName="flightDurationHour"
            [parentForm]="flightDetailsForm()"
            [options]="initializeFlightFormOptions().hours"
            fieldName="flightDurationHour"
            label="Flight Duration"
            placeholder="Hour"
          ></shared-select>
          <span class="colon">:</span>
          <shared-select
            class="time"
            formControlName="flightDurationMinutes"
            [parentForm]="flightDetailsForm()"
            [options]="initializeFlightFormOptions().minutes"
            fieldName="flightDurationMinutes"
            placeholder="Minutes"
          ></shared-select>
        </div>
      </div>
    </form>

    <form [formGroup]="aircraftDetailsForm()">
      <shared-select
        formControlName="airline"
        [parentForm]="aircraftDetailsForm()"
        [options]="initializeFlightFormOptions().airlines"
        fieldName="airline"
        label="Airline"
        placeholder="Airline"
      ></shared-select>
      <shared-select
        formControlName="aircraftType"
        [parentForm]="aircraftDetailsForm()"
        [options]="initializeFlightFormOptions().aircrafts"
        fieldName="aircraftType"
        label="Aircraft Type"
        placeholder="Aircraft Type"
        (emitChangedValue)="updateAircraftRegNumber($event.regNumber)"
      ></shared-select>
      <shared-input
        formControlName="aircraftReg"
        [parentForm]="aircraftDetailsForm()"
        [isReadOnly]="true"
        fieldName="aircraftReg"
        label="Aircraft Reg."
        placeholder="Aircraft Reg."
      ></shared-input>
    </form>
  </div>`,
  styleUrl: './flight-form.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FlightFormComponent {
  flightDetailsForm = input.required<any>();
  aircraftDetailsForm = input.required<any>();
  initializeFlightFormOptions = input<any>();

  protected updateAircraftRegNumber(regNumber: string) {
    this.aircraftDetailsForm().patchValue({
      aircraftReg: regNumber,
    });
  }
}
