import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'auth-banner',
  standalone: true,
  template: `
    <div class="banner">
      <img src="global/assets/images/banner-login.png" />
      <div class="shadow"></div>
    </div>
  `,
  styleUrl: './banner.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BannerComponent {}
