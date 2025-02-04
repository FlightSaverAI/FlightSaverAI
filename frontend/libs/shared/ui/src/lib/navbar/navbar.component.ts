import { Component, inject, input, OnInit } from '@angular/core';
import { NgClass, NgIf } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';
import { RouterModule } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';
import { Router } from '@angular/router';
import { DropdownDirective } from '@shared/ui-components';
import { CookieService } from 'ngx-cookie-service';

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
  imports: [NgClass, ButtonComponent, RouterModule, NgIf, NgOptimizedImage, DropdownDirective],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit {
  navConfig = input.required<NavConfig[]>();
  dropdownConfig = input<any[]>();

  activeIndex = 0;
  isNavbarOpen!: boolean;

  private _router = inject(Router);
  private _cookieService = inject(CookieService);
  private _routeUrls: any = {
    Profile: '/authorized/user-profile',
    Settings: '/authorized/user-profile/settings',
    Logout: '/login',
  };

  public ngOnInit(): void {
    this.activeIndex = this._findActiveNavIndex();
  }

  private _findActiveNavIndex() {
    return this.navConfig().findIndex(({ name }) => this._router.url.includes(name.toLowerCase()));
  }

  public openNavbar() {
    this.isNavbarOpen = !this.isNavbarOpen;
  }

  public navigateToPassedUrl(field: string) {
    this._router.navigateByUrl(this._routeUrls[field]);

    if (field === 'Logout') {
      this._cookieService.delete('AuthToken');

      setTimeout(() => {
        this._router.navigateByUrl('/login');
        document.location.reload();
      }, 0);
    }
  }
}
