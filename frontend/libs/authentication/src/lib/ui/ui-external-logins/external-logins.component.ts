import { NgOptimizedImage } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'auth-external-logins',
  standalone: true,
  imports: [NgOptimizedImage],
  template: `
    <div class="external-logins-container">
      <p>or continue with</p>
      <div class="external-logins">
        <div class="external-logins__login">
          <img ngSrc="global/assets/facebook.svg" alt="apple" width="25" height="25" />
        </div>
        <div class="external-logins__login">
          <img ngSrc="global/assets/apple.svg" alt="apple" width="16" height="16" />
        </div>
        <div class="external-logins__login">
          <img ngSrc="global/assets/google.svg" alt="facebook" width="16" height="16" />
        </div>
      </div>
    </div>
  `,
  styleUrl: './external-logins.component.scss',
})
export class ExternalLoginsComponent {}
