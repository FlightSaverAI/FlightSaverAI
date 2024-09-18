import { Component } from '@angular/core';
import { RegistrationComponent } from '@flight-saver/authentication/ui';
import { registrationForm } from '@flight-saver/authentication/utils';

@Component({
  standalone: true,
  imports: [RegistrationComponent],
  template: `
    <auth-registration
      [registrationForm]="registrationForm"
      (confirmRegistration)="confirmRegistration()"
    ></auth-registration>
  `,
})
export class RegistrationContainerComponent {
  readonly registrationForm = registrationForm();

  confirmRegistration() {
    if (this.registrationForm.valid) {
      console.log('Form submitted', this.registrationForm.value);
    } else {
      console.log('Form is invalid');
    }
  }
}
