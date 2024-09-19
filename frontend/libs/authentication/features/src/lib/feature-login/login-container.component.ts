import { Component, inject } from '@angular/core';
import { LoginComponent } from '@flight-saver/authentication/ui';
import { loginForm } from '@flight-saver/authentication/utils';
import { AuthFacadeService } from '@flight-saver/authentication/data-access';

@Component({
  standalone: true,
  imports: [LoginComponent],
  template: ` <auth-login [loginForm]="loginForm" (confirmLogin)="confirmLogin()"></auth-login> `,
})
export class LoginContainerComponent {
  private _authFacade = inject(AuthFacadeService);
  readonly loginForm = loginForm();

  confirmLogin() {
    if (this.loginForm.invalid) {
      return;
    }

    this._authFacade.login(this.loginForm.getRawValue());
  }
}
