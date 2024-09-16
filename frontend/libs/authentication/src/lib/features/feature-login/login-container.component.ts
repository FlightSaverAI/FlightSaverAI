import { Component } from '@angular/core';
import { LoginComponent } from '../../ui/ui-login/login.component';
import { loginForm } from '../../utils/login-form';

@Component({
  standalone: true,
  imports: [LoginComponent],
  template: ` <auth-login [loginForm]="loginForm" (confirmLogin)="confirmLogin()"></auth-login> `,
})
export class LoginContainerComponent {
  readonly loginForm = loginForm();

  confirmLogin() {
    console.log(this.loginForm.getRawValue());
  }
}
