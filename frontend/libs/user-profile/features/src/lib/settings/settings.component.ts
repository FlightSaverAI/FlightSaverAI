import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvatarComponent } from '@flight-saver/user-profile/ui';
import { UserProfileEditComponent } from '@flight-saver/user-profile/ui';
import { SettingsService } from '@flight-saver/user-profile/data-access';
import { toSignal } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { changePasswordForm, userProfileUpdateForm } from '@flight-saver/user-profile/utils';
import { tap } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, AvatarComponent, UserProfileEditComponent],
  template: `
    @defer(when userData()) {
    <div class="settings-container">
      <user-profile-avatar [isSettingsSection]="true"></user-profile-avatar>
      <user-profile-edit
        [userData]="userData()"
        [userProfileForm]="userProfileForm()"
        [changePasswordForm]="changePasswordForm()"
        (save)="saveUserData()"
      ></user-profile-edit>
    </div>
    }
  `,
  styleUrl: './settings.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SettingsService],
})
export class SettingsComponent {
  private _settingsService = inject(SettingsService);
  private _router = inject(Router);

  protected userProfileForm = signal(userProfileUpdateForm());
  protected changePasswordForm = signal(changePasswordForm());

  protected userData = toSignal(
    this._settingsService.getUserProfileData().pipe(
      tap((userData) =>
        this.userProfileForm().patchValue({
          username: userData.username,
          email: userData.email,
        })
      )
    )
  );

  protected saveUserData() {
    if (this.changePasswordForm().valid) {
      this._settingsService
        .updateUserProfileData(this.changePasswordForm().getRawValue())
        .subscribe({
          next: () => {
            alert('Successfuly updated password');
            this._router.navigateByUrl('/authorized/user-profile');
          },
          error: () => {
            alert('Error');
          },
        });
    } else {
      alert('Invalid form');
    }
  }
}
