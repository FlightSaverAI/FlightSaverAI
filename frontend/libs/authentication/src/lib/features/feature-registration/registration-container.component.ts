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
    console.log(this.registrationForm.getRawValue());
  }
}
