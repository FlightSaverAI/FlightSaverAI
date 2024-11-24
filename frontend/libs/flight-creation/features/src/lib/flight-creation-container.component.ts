import { Component, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  FlightFormComponent,
  StepperComponent,
  TicketFormComponent,
} from '@flight-saver/flight-creation/ui';
import { ButtonComponent } from '@shared/ui-components';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FlightCreationConstants } from './constants/flight-creation.constants';

export const MAX_STEPS = 3;

@Component({
  standalone: true,
  imports: [CommonModule, StepperComponent, ButtonComponent, RouterModule],
  template: `<div class="container">
    <flight-creation-stepper
      [stepperConfiguration]="stepperConfig()"
      [currentStep]="currentStep"
    ></flight-creation-stepper>
    <router-outlet
      (activate)="handleComponentActivation($event)"
      (deactivate)="handleComponentDeactivation()"
    ></router-outlet>
    <div class="buttons">
      <shared-button
        content="Previous"
        [ngStyle]="currentStep > 1 ? { visibility: 'visible' } : { visibility: 'hidden' }"
        [imgConf]="prevBtnConfig()"
        (emitEvent)="navigateToPreviousStep()"
      ></shared-button>
      <shared-button
        content="Next"
        [imgConf]="nextBtnConfig()"
        (emitEvent)="navigateToNextStep()"
      ></shared-button>
    </div>
  </div>`,
  styleUrl: './flight-creation-container.component.scss',
})
export class FlightCreationContainerComponent {
  private _router = inject(Router);
  private _activatedRoute = inject(ActivatedRoute);
  private _queryParams = toSignal(inject(ActivatedRoute).queryParams);

  prevBtnConfig = signal(FlightCreationConstants.prevBtnConf);
  nextBtnConfig = signal(FlightCreationConstants.nextBtnConf);
  stepperConfig = signal(FlightCreationConstants.stepperConf);

  currentStep = 1;
  activeComponent: unknown;
  forms: any = {};

  queryParamsEffect = effect(() => {
    this.currentStep = parseInt(this._queryParams()?.['stepNumber']);
    this.forms = JSON.parse(sessionStorage.getItem('formsState') || '{}');
  });

  public handleComponentActivation(component: unknown) {
    this.activeComponent = component;
  }

  public handleComponentDeactivation() {
    this._getStepComponentForm(this.activeComponent);

    sessionStorage.setItem('formsState', JSON.stringify(this.forms));
  }

  public navigateToPreviousStep() {
    if (this.currentStep <= 1) {
      return;
    }

    this.updateStep(this.currentStep - 1);
  }

  public navigateToNextStep() {
    if (this.currentStep >= MAX_STEPS) {
      return;
    }

    this.updateStep(this.currentStep + 1);
  }

  private updateStep(step: number) {
    const stepConfig = this.stepperConfig()[step - 1];

    if (!stepConfig) {
      return;
    }

    this._router.navigate([stepConfig.stepName], {
      relativeTo: this._activatedRoute,
      queryParams: { stepNumber: step },
      queryParamsHandling: 'merge',
    });
  }

  private _getStepComponentForm(component: unknown) {
    if (component instanceof FlightFormComponent) {
      this.forms.flightDetailsForm = component.flightDetailsForm.getRawValue();
      this.forms.aircraftDetailsForm = component.aircraftDetailsForm.getRawValue();
    }

    if (component instanceof TicketFormComponent) {
      this.forms.ticketForm = component.ticketForm.getRawValue();
    }
  }
}
