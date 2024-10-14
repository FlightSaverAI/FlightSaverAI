import { Component } from '@angular/core';

@Component({
  selector: 'auth-banner',
  standalone: true,
  imports: [],
  template: `
    <div class="banner">
      <img src="global/assets/images/banner-login.png" />
      <div class="shadow"></div>
    </div>
  `,
  styleUrl: './banner.component.scss',
})
export class BannerComponent {}
