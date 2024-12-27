import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarComponent, NavConfig } from '@shared/ui-components';

@Component({
  selector: 'app-authorized-user',
  standalone: true,
  imports: [RouterModule, NavbarComponent],
  template: `
    <shared-navbar [navConfig]="navConfig"></shared-navbar>
    <main>
      <router-outlet></router-outlet>
    </main>
  `,
  styleUrl: './authorized-user.component.scss',
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
}
