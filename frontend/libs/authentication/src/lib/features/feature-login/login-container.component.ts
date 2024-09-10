import { Component } from '@angular/core';
import { LoginComponent } from '../../ui/ui-login/login.component';

@Component({
  standalone: true,
  imports: [LoginComponent],
  template: ` <auth-login></auth-login> `,
})
export class LoginContainerComponent {}
