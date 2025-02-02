import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvatarComponent, CroppedPhotoModalComponent } from '@flight-saver/user-profile/ui';
import { UserProfileEditComponent } from '@flight-saver/user-profile/ui';
import { UserProfileService } from '@flight-saver/user-profile/data-access';
import { toSignal } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { changePasswordForm, userProfileUpdateForm } from '@flight-saver/user-profile/utils';
import { concatMap, filter, tap } from 'rxjs';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

@Component({
  standalone: true,
  imports: [CommonModule, AvatarComponent, UserProfileEditComponent, MatDialogModule],
  template: `
    @defer(when userData()) {
    <div class="settings-container">
      <user-profile-avatar
        [isSettingsSection]="true"
        [backgroundPhotoUrl]="
          updatedUserPhotos()?.backgroundPictureUrl ?? userData().backgroundPictureUrl
        "
        [profilePhotoUrl]="updatedUserPhotos()?.profilePictureUrl ?? userData().profilePictureUrl"
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
  providers: [UserProfileService],
})
export class SettingsComponent {
  private _userProfileService = inject(UserProfileService);
  private _dialog = inject(MatDialog);
  private _router = inject(Router);

  protected userProfileForm = signal(userProfileUpdateForm());
  protected changePasswordForm = signal(changePasswordForm());
  protected userData = toSignal(
    this._userProfileService.getUserProfileData().pipe(
      tap(({ username, email }) =>
        this.userProfileForm().patchValue({
          username: username,
          email: email,
        })
      )
    )
  );
  protected updatedUserPhotos = signal<{
    profilePictureUrl?: string;
    backgroundPictureUrl?: string;
  } | null>(null);

  protected saveUserData() {
    if (this.changePasswordForm().invalid) {
      alert('Invalid form');
      return;
    }

    this._userProfileService.updatePassword(this.changePasswordForm().getRawValue()).subscribe({
      next: () => alert('Successfuly updated password'),
      error: () => alert('Error'),
      complete: () => this._router.navigateByUrl('/authorized/user-profile'),
    });
  }

  protected openUploadPhotoModal(photoType: 'Background' | 'Profile') {
    const modalRef = this._dialog.open(CroppedPhotoModalComponent, {
      width: '700px',
      data: { photoType },
    });

    modalRef
      .afterClosed()
      .pipe(
        filter((blob) => !!blob),
        concatMap((blob) => this._userProfileService.updateUserPhoto(blob, photoType))
      )
      .subscribe((newUserPhoto) => this.updatedUserPhotos.set(newUserPhoto));
  }
}
