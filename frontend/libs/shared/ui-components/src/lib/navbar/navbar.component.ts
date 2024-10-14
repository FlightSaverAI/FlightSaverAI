import { Component, input } from '@angular/core';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { ButtonComponent } from '../button/button.component';
import { RouterModule } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';

export type NavTypes = 'button' | 'list' | 'photo';

export interface NavConfig {
  type: NavTypes;
  name: string;
  routerLink: string;
  image?: ImageNavConfig;
}

export interface ImageNavConfig {
  src: string;
  alt: string;
  width: number;
  height: number;
}

@Component({
  selector: 'shared-navbar',
  standalone: true,
  imports: [NgClass, ButtonComponent, NgFor, RouterModule, NgIf, NgOptimizedImage],
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
