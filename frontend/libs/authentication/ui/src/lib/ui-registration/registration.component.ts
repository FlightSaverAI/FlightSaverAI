import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { BannerComponent } from '../ui-banner/banner.component';
import { ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from '@shared/ui-components';
import { ButtonComponent } from '@shared/ui-components';
import { RegistrationForm } from '@flight-saver/authentication/utils';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'auth-registration',
  standalone: true,
  imports: [BannerComponent, InputComponent, ButtonComponent, ReactiveFormsModule, RouterLink],
  template: ` <section>
    <img class="line2" src="global/assets/line2.svg" alt="" />
    <img class="line1" src="global/assets/line1.svg" alt="" />
    <div class="wrapper">
      <div class="logo">
        <img src="global/assets/flight-saver-logo.svg" alt="logo" />
        <p class="logo__title">FlightSaver</p>
      </div>
      <auth-banner></auth-banner>
      <div class="registration-container">
        <div class="registration">
          <h2 class="registration__title">Sign up</h2>
          <p class="registration__subtitle">
            If you already have an account register <br />
            You can<a routerLink="/login">Login here !</a>
          </p>

          <form
            [formGroup]="registrationForm()"
            class="registration__form"
            (ngSubmit)="confirmRegistration.emit()"
          >
            <shared-input
              formControlName="username"
              iconSrc="global/assets/email.svg"
              placeholder="Enter your user name"
              label="User Name"
              [parentForm]="registrationForm()"
              fieldName="username"
            ></shared-input>
            <shared-input
              formControlName="email"
              iconSrc="global/assets/email.svg"
              placeholder="Enter your email adress"
              label="Email"
              [parentForm]="registrationForm()"
              fieldName="email"
            ></shared-input>
            <shared-input
              formControlName="password"
              iconSrc="global/assets/email.svg"
              placeholder="Enter your password"
              label="Password"
              type="password"
              [parentForm]="registrationForm()"
              fieldName="password"
            ></shared-input>
            <shared-input
              formControlName="confirmPassword"
              iconSrc="global/assets/password.svg"
              placeholder="Confirm your password"
              label="Confirm Password"
              type="password"
              [parentForm]="registrationForm()"
              fieldName="confirmPassword"
            ></shared-input>
            <shared-button content="Register" type="submit"></shared-button>
          </form>
        </div>
      </div>
    </div>
  </section>`,
  styleUrl: './registration.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistrationComponent {
  registrationForm = input.required<RegistrationForm>();
  confirmRegistration = output();
}
