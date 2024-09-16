import { Component } from '@angular/core';
import { RegistrationComponent } from '../../ui/ui-registration/registration.component';
import { registrationForm } from '../../utils/registration-form';

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
