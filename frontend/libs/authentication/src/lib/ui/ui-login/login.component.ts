import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BannerComponent } from '../ui-banner/banner.component';
import { NgOptimizedImage } from '@angular/common';
import { InputComponent } from '@shared/ui-components';
import { ButtonComponent } from '@shared/ui-components';
import { ExternalLoginsComponent } from '../ui-external-logins/external-logins.component';

@Component({
  selector: 'auth-login',
  standalone: true,
  imports: [
    BannerComponent,
    NgOptimizedImage,
    InputComponent,
    ButtonComponent,
    ExternalLoginsComponent,
  ],
  template: `
    <section>
      <img class="line2" src="global/assets/line2.svg" alt="" />
      <img class="line1" src="global/assets/line1.svg" alt="" />
      <div class="wrapper">
        <div class="logo">
          <img src="global/assets/flight-saver-logo.svg" alt="logo" />
          <p class="logo__title">FlightSaver</p>
        </div>
        <auth-banner></auth-banner>
        <div class="login-container">
          <div class="login">
            <h2 class="login__title">Sign in</h2>
            <p class="login__subtitle">
              If you don’t have an account register <br />
              You can<a href="/register">Register here !</a>
            </p>

            <form class="login__form">
              <shared-input
                iconSrc="global/assets/email.svg"
                placeholder="Enter your email adress"
                label="Email"
              ></shared-input>
              <shared-input
                iconSrc="global/assets/password.svg"
                placeholder="Enter your password"
                label="Password"
                type="password"
              ></shared-input>
              <div class="login__form-options">
                <div>
                  <input type="checkbox" />
                  <label style="margin-left: 5px;" for="remember">Remember me</label>
                </div>
                <a href="/forgot-password">Forgot password ?</a>
              </div>
              <shared-button content="Login" type="submit"></shared-button>
            </form>
            <auth-external-logins></auth-external-logins>
          </div>
        </div>
      </div>
    </section>
  `,
  styleUrl: './login.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {}
