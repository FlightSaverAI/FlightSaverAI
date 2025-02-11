import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { AuthFacadeService } from '@flight-saver/authentication/data-access';
import { RegistrationComponent } from '@flight-saver/authentication/ui';
import { registrationForm } from '@flight-saver/authentication/utils';
import { AlertService } from '@shared/data-access';

@Component({
  standalone: true,
  imports: [RegistrationComponent],
  template: `
    <auth-registration
      [registrationForm]="registrationForm()"
      (confirmRegistration)="confirmRegistration()"
    ></auth-registration>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistrationContainerComponent {
  private _authFacade = inject(AuthFacadeService);
  private _alertService = inject(AlertService);

  protected readonly registrationForm = signal(registrationForm());

  protected confirmRegistration() {
    if (this.registrationForm().invalid) {
      this._alertService.showAlert('warning', 'Form is invalid');
      return;
    }

    this._authFacade.registration(this.registrationForm().getRawValue());
  }
}
