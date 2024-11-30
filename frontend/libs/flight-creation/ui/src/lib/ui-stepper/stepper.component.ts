import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface StepperConfiguration {
  stepName: string;
  stepNumber: number;
}

@Component({
  selector: 'flight-creation-stepper',
  standalone: true,
  imports: [CommonModule],
  template: `<div class="stepper">
    @for(element of stepperConfiguration(); track element;){
    <div
      class="stepper__option"
      [ngClass]="
        currentStep() >= element.stepNumber
          ? 'stepper__option--active'
          : 'stepper__option--disabled'
      "
    >
      <div class="stepper__option-number">
        {{ element.stepNumber }}
      </div>
      <div
        class="stepper__option-name"
        [ngClass]="currentStep() === element.stepNumber ? 'stepper__option-name--active' : ''"
      >
        {{ element.stepName | titlecase }}
      </div>
    </div>
    }
  </div>`,
  styleUrl: './stepper.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StepperComponent {
  stepperConfiguration = input.required<StepperConfiguration[]>();
  currentStep = input.required<number>();
}
