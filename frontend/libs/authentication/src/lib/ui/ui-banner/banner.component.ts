import { Component } from '@angular/core';

@Component({
  selector: 'auth-banner',
  standalone: true,
  imports: [],
  template: ` <img src="global/assets/images/banner-login.png" />
    <div class="shadow"></div>`,
  styleUrl: './banner.component.scss',
})
export class BannerComponent {}
