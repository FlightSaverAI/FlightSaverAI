import { Component, inject } from '@angular/core';
import { AuthFacadeService } from '@flight-saver/authentication/data-access';
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

  private _authFacade = inject(AuthFacadeService);

  confirmRegistration() {
    if (this.registrationForm.invalid) {
      return;
    }

    this._authFacade.registration(this.registrationForm.getRawValue());
  }
}
