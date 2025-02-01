import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvatarComponent, CroppedPhotoModalComponent } from '@flight-saver/user-profile/ui';
import { UserProfileEditComponent } from '@flight-saver/user-profile/ui';
import { SettingsService } from '@flight-saver/user-profile/data-access';
import { toSignal } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { changePasswordForm, userProfileUpdateForm } from '@flight-saver/user-profile/utils';
import { concatMap, filter, tap } from 'rxjs';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

@Component({
  standalone: true,
  imports: [CommonModule, AvatarComponent, UserProfileEditComponent, MatDialogModule],
  template: `
    <!-- TOFIX -->
    @defer(when userData() || updatedUserData()) {
    <div class="settings-container">
      <user-profile-avatar
        [isSettingsSection]="true"
        [backgroundPhotoUrl]="
          updatedUserData().backgroundPictureUrl ?? userData().backgroundPictureUrl
        "
        [profilePhotoUrl]="updatedUserData().profilePictureUrl ?? userData().profilePictureUrl"
        (openUploadPhotoModal)="openUploadPhotoModal($event)"
      ></user-profile-avatar>
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
  private _dialog = inject(MatDialog);
  private _router = inject(Router);

  protected userProfileForm = signal(userProfileUpdateForm());
  protected changePasswordForm = signal(changePasswordForm());

  //TOFIX
  protected updatedUserData = signal({} as any);

  protected userData = toSignal(
    this._settingsService.getUserProfileData().pipe(
      tap(({ username, email }) =>
        this.userProfileForm().patchValue({
          username: username,
          email: email,
        })
      )
    )
  );

  protected saveUserData() {
    if (this.changePasswordForm().invalid) {
      alert('Invalid form');
      return;
    }

    this._settingsService.updateUserProfileData(this.changePasswordForm().getRawValue()).subscribe({
      next: () => alert('Successfuly updated password'),
      error: () => alert('Error'),
      complete: () => this._router.navigateByUrl('/authorized/user-profile'),
    });
  }

  // TOFIX
  protected openUploadPhotoModal(photoType: 'Background' | 'Profile') {
    const modalRef = this._dialog.open(CroppedPhotoModalComponent, {
      width: '700px',
      data: { photoType },
    });

    modalRef
      .afterClosed()
      .pipe(
        filter((blob) => !!blob),
        concatMap((blob) => this._settingsService.updateUserProfileData(blob, photoType)),
        concatMap(() => this._settingsService.getUserProfileData())
      )
      .subscribe((newUserData) => this.updatedUserData.set(newUserData));
  }
}
