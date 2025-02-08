import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarComponent, NavConfig } from '@shared/ui';

@Component({
  standalone: true,
  imports: [RouterModule, NavbarComponent],
  template: `
    <shared-navbar [navConfig]="navConfig" [dropdownConfig]="dropdownConfig"></shared-navbar>
    <main>
      <router-outlet></router-outlet>
    </main>
  `,
})
export class AuthorizedUserComponent {
  navConfig: NavConfig[] = [
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
      name: 'Friends',
      routerLink: '/authorized/friends',
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
  ];

  dropdownConfig = [
    {
      field: 'Profile',
    },
    {
      field: 'Settings',
    },
    {
      field: 'Logout',
    },
  ];
}
