import { Component, inject, input, OnInit } from '@angular/core';
import { NgClass, NgIf } from '@angular/common';
import { ButtonComponent } from '../button/button.component';
import { RouterModule } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';
import { Router } from '@angular/router';
import { ProfileDropdownDirective } from '../profile-dropdown/profile-dropdown.directive';

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
  imports: [
    NgClass,
    ButtonComponent,
    RouterModule,
    NgIf,
    NgOptimizedImage,
    ProfileDropdownDirective,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit {
  navConfig = input.required<NavConfig[]>();

  activeIndex = 0;
  isNavbarOpen!: boolean;

  private _router = inject(Router);

  public ngOnInit(): void {
    this.activeIndex = this._findActiveNavIndex();
  }

  private _findActiveNavIndex() {
    return this.navConfig().findIndex(({ name }) => this._router.url.includes(name.toLowerCase()));
  }

  public openNavbar() {
    this.isNavbarOpen = !this.isNavbarOpen;
  }

  public navigateToPassedUrl(url: string, index: number) {
    this.activeIndex = index;
    this._router.navigateByUrl(url);
  }
}
