import { Component, inject, signal } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavConfig } from '@shared/models';
import { NavbarComponent } from '@shared/ui';
import { CookieService } from 'ngx-cookie-service';

@Component({
  standalone: true,
  imports: [RouterModule, NavbarComponent],
  template: `
    <shared-navbar [navConfig]="navConfig()" [dropdownConfig]="navDropdownConfig()"></shared-navbar>
    <main>
      <router-outlet></router-outlet>
    </main>
  `,
})
export class AuthorizedUserComponent {
  private _cookieService = inject(CookieService);
  private _router = inject(Router);

  protected navConfig = signal<NavConfig[]>([
    {
      type: 'list',
      name: 'Home',
      routerLink: '/authorized/home',
    },
    {
      type: 'list',
      name: 'Statistics',
      routerLink: '/authorized/statistics',
    },
    {
      type: 'list',
      name: 'Community',
      routerLink: '/authorized/community',
    },
    {
      type: 'list',
      name: 'Search',
      routerLink: '/authorized/users-search',
    },
    {
      type: 'button',
      name: 'Add Flight',
      routerLink: '/authorized/flight-creation',
    },
    {
      type: 'photo',
      name: '',
      routerLink: '/authorized/user-profile',
      image: {
        src: 'global/assets/images/user-photo.png',
        alt: 'User Photo',
        width: 50,
        height: 50,
      },
    },
  ]);

  protected navDropdownConfig = signal([
    {
      field: 'Profile',
      action: () => this._router.navigateByUrl('/authorized/user-profile'),
    },
    {
      field: 'Settings',
      action: () => this._router.navigateByUrl('/authorized/user-profile/settings'),
    },
    {
      field: 'Logout',
      action: () => {
        this._cookieService.delete('AuthToken', '/');
        this._router.navigateByUrl('/login');
        setTimeout(() => {
          document.location.reload();
        }, 0);
      },
    },
  ]);
}
