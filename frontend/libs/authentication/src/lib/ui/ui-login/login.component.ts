import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BannerComponent } from '../ui-banner/banner.component';
import { NgOptimizedImage } from '@angular/common';
import { InputComponent } from '@shared/ui-components';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'auth-login',
  standalone: true,
  imports: [BannerComponent, NgOptimizedImage, InputComponent, ButtonComponent],
  template: `
    <section>
      <div class="wrapper">
        <div class="logo">
          <img src="global/assets/flight-saver-logo.svg" alt="logo" />
          <p class="logo__title">FlightSaver</p>
        </div>
        <auth-banner class="banner"></auth-banner>
        <div class="login">
          <div style="margin-bottom: 10px;">
            <h2 class="login__title">Sign in</h2>
            <p class="login__subtitle">
              If you don’t have an account register <br />
              You can<a href="/register">Register here !</a>
            </p>
          </div>

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

          <div class="login__alternative">
            <p style="margin: 15px 0">or continue with</p>
            <div class="login__alternative-options">
              <div class="hehe">
                <img ngSrc="global/assets/facebook.svg" alt="apple" width="25" height="25" />
              </div>
              <div class="hehe">
                <img ngSrc="global/assets/apple.svg" alt="apple" width="16" height="16" />
              </div>
              <div class="hehe">
                <img ngSrc="global/assets/google.svg" alt="facebook" width="16" height="16" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  `,
  styleUrl: './login.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {}
