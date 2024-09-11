import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BannerComponent } from '../ui-banner/banner.component';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'auth-login',
  standalone: true,
  imports: [BannerComponent, NgOptimizedImage],
  template: `
    <div class="login">
      <div class="login__logo">
        <img ngSrc="global/assets/flight-saver-logo.svg" alt="logo" width="50" height="50" />
        <p class="login__logo-title">FlightSaver</p>
      </div>
      <auth-banner class="login__banner"></auth-banner>
      <div class="login__content">
        <div style="margin-bottom: 25px;">
          <h1 class="login__title">Sign in</h1>
          <p class="login__subtitle">
            If you don’t have an account register <br />
            You can<a href="/register">Register here !</a>
          </p>
        </div>

        <form class="login__form">
          <input type="text" placeholder="Email" />
          <input type="password" placeholder="Password" />
          <div class="login__form-options" style="margin: 15px 0;">
            <div>
              <input type="checkbox" />
              <label style="margin-left: 5px;" for="remember">Remember me</label>
            </div>
            <a href="/forgot-password">Forgot password ?</a>
          </div>
          <button>Login</button>
        </form>

        <div class="login__alternatives">
          <p style="margin: 20px 0">or continue with</p>
          <div class="login__alternatives-options">
            <div class="hehe">
              <img ngSrc="global/assets/facebook.svg" alt="apple" width="18" height="18" />
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
  `,
  styleUrl: './login.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {}
