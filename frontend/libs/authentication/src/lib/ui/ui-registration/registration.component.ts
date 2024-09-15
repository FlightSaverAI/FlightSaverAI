import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'auth-registration',
  standalone: true,
  imports: [],
  template: ` <p>registration works!</p> `,
  styleUrl: './registration.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistrationComponent {}
