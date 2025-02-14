import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { LoginComponent } from '@flight-saver/authentication/ui';
import { loginForm } from '@flight-saver/authentication/utils';
import { AuthFacadeService } from '@flight-saver/authentication/data-access';
import { AlertService } from '@shared/data-access';

@Component({
  standalone: true,
  imports: [LoginComponent],
  template: ` <auth-login [loginForm]="loginForm" (confirmLogin)="confirmLogin()"></auth-login> `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginContainerComponent {
  private _authFacade = inject(AuthFacadeService);
  private _alertService = inject(AlertService);

  protected readonly loginForm = loginForm();

  protected confirmLogin() {
    if (this.loginForm.invalid) {
      this._alertService.showAlert('warning', 'Form is invalid');
      return;
    }

    this._authFacade.login(this.loginForm.getRawValue());
  }
}
