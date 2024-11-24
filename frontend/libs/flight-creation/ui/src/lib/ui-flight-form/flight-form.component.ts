import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from '@shared/ui-components';
import { aircraftDetailsForm, flightDetailsForm } from '@flight-saver/flight-creation/utils';

@Component({
  selector: 'flight-creation-flight-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputComponent],
  template: `<div class="wrapper">
    <form [formGroup]="flightDetailsForm">
      <div class="part1">
        <shared-input
          formControlName="departureDate"
          [parentForm]="flightDetailsForm"
          fieldName="departureDate"
          label="Departure Date"
          placeholder="Departure Date"
        ></shared-input>
        <shared-input
          formControlName="departureAirport"
          [parentForm]="flightDetailsForm"
          fieldName="departureAirport"
          label="Departure Airport"
          placeholder="Departure Airport"
        ></shared-input>
        <shared-input
          formControlName="arrivalAirport"
          [parentForm]="flightDetailsForm"
          fieldName="arrivalAirport"
          label="Arrival Airport"
          placeholder="Arrival Airport"
        ></shared-input>
      </div>
      <div class="part2">
        <shared-input
          formControlName="flightNumber"
          [parentForm]="flightDetailsForm"
          fieldName="flightNumber"
          label="Flight Number"
          placeholder="Flight Number"
        ></shared-input>
        <div class="check">
          <shared-input
            class="hehe"
            formControlName="departureTimeHour"
            [parentForm]="flightDetailsForm"
            fieldName="departureTimeHour"
            label="Departure Time"
            placeholder="Hour"
          ></shared-input>
          <span class="colon">:</span>
          <shared-input
            class="hehe"
            formControlName="departureTimeMinutes"
            [parentForm]="flightDetailsForm"
            fieldName="departureTimeMinutes"
            placeholder="Minutes"
          ></shared-input>
        </div>
        <div class="check">
          <shared-input
            class="hehe"
            formControlName="arrivalTimeHour"
            [parentForm]="flightDetailsForm"
            fieldName="arrivalTimeHour"
            label="Arrival Time"
            placeholder="Hour"
          ></shared-input>
          <span class="colon">:</span>
          <shared-input
            class="hehe"
            formControlName="arrivalTimeMinutes"
            [parentForm]="flightDetailsForm"
            fieldName="arrivalTimeMinutes"
            placeholder="Minutes"
          ></shared-input>
        </div>
        <shared-input
          formControlName="flightDuration"
          [parentForm]="flightDetailsForm"
          fieldName="flightDuration"
          label="Flight Duration"
          placeholder="Flight Duration"
        ></shared-input>
      </div>
    </form>

    <form [formGroup]="aircraftDetailsForm">
      <shared-input
        formControlName="airline"
        [parentForm]="aircraftDetailsForm"
        fieldName="airline"
        label="Airline"
        placeholder="Airline"
      ></shared-input>
      <shared-input
        formControlName="aircraftType"
        [parentForm]="aircraftDetailsForm"
        fieldName="aircraftType"
        label="Aircraft Type"
        placeholder="Aircraft Type"
      ></shared-input>
      <shared-input
        formControlName="aircraftReg"
        [parentForm]="aircraftDetailsForm"
        fieldName="aircraftReg"
        label="Aircraft Reg."
        placeholder="Aircraft Reg."
      ></shared-input>
    </form>
  </div>`,
  styleUrl: './flight-form.component.scss',
})
export class FlightFormComponent {
  public flightDetailsForm = flightDetailsForm();
  public aircraftDetailsForm = aircraftDetailsForm();
}
