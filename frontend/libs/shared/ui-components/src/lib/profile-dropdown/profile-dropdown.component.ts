import { Component, inject, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'shared-profile-dropdown',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `<div class="dropdown">
    <ul class="dropdown__list">
      <li class="dropdown__item" (click)="selectOption.emit(userProfileUrl)">
        <p class="dropdown__text">Profile</p>
      </li>
      <li class="dropdown__item">
        <p class="dropdown__text">Settings</p>
      </li>
      <li class="dropdown__item" (click)="logout()">
        <p class="dropdown__text">Logout</p>
      </li>
    </ul>
  </div> `,
  styleUrl: './profile-dropdown.component.scss',
})
export class ProfileDropdownComponent {
  selectOption = output<string>();

  protected userProfileUrl = '/authorized/user-profile';

  private _cookieService = inject(CookieService);
  private _router = inject(Router);

  logout() {
    this._cookieService.delete('AuthToken');
    this._router.navigateByUrl('/login');

    setTimeout(() => {
      document.location.reload();
    }, 0);
  }
}
