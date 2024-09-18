import { Component, inject } from '@angular/core';
import { LoginComponent } from '../../ui/ui-login/login.component';
import { loginForm } from '../../utils/login-form';
import { AuthFacadeService } from '../../data-access/auth-facade.service';

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
