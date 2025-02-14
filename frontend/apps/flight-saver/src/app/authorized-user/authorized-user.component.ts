import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '@shared/data-access';
import { NavConfig } from '@shared/models';
import { NavbarComponent } from '@shared/ui';
import { CookieService } from 'ngx-cookie-service';

@Component({
  standalone: true,
  imports: [RouterModule, NavbarComponent],
  template: `
    <shared-navbar
      [userPhotoSrc]="updatedUserPhoto() ?? userPhoto().profilePictureUrl"
      [navConfig]="navConfig()"
      [dropdownConfig]="navDropdownConfig()"
    ></shared-navbar>
    <main>
      <router-outlet></router-outlet>
    </main>
  `,
})
export class AuthorizedUserComponent implements OnInit {
  private _cookieService = inject(CookieService);
  private _router = inject(Router);
  private _userService = inject(UserService);
  private _destroyRef = inject(DestroyRef);

  protected userPhoto = toSignal(inject(UserService).getUserProfileData());
 protected updatedUserPhoto = signal<string | null>(null);

  public ngOnInit() {
    this._userService.userPhoto
      .pipe(takeUntilDestroyed(this._destroyRef))
      .subscribe((updatedUserPhoto) => this.updatedUserPhoto.set(updatedUserPhoto));
  }

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
