import { Component, input } from '@angular/core';
import { NgClass, NgFor } from '@angular/common';
import { ButtonComponent } from '../button/button.component';
import { RouterModule } from '@angular/router';

export type NavTypes = 'button' | 'list';

export interface NavConfig {
  type: NavTypes;
  name: string;
  routerLink: string;
}

@Component({
  selector: 'shared-navbar',
  standalone: true,
  imports: [NgClass, ButtonComponent, NgFor, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  navConfig = input.required<NavConfig[]>();

  isNavbarOpen!: boolean;

  public openNavbar() {
    this.isNavbarOpen = !this.isNavbarOpen;
  }
}
