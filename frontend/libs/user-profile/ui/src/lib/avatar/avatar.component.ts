import { ChangeDetectionStrategy, Component, input, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';
import { RouterModule } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'user-profile-avatar',
  standalone: true,
  imports: [CommonModule, ButtonComponent, RouterModule, NgOptimizedImage],
  template: `
    <div class="container">
      <img
        class="background"
        [src]="backgroundPhotoUrl()"
        [style.--brightness-value]="isSettingsSection() ? 0.5 : 0.7"
        alt="background"
      />
      @if(isSettingsSection()) {
      <shared-button
        class="background-update-btn"
        content="Update Background"
        category="secondary"
        (emitEvent)="openUploadPhotoModal.emit('Background')"
      ></shared-button>
      }
      <div class="avatar">
        <img
          class="avatar-picture"
          [ngSrc]="profilePhotoUrl() || defaultUserPhoto()"
          width="250"
          height="250"
          alt=""
        />
        @if(isSettingsSection()) {
        <shared-button
          class="update-picture"
          content="Update Photo"
          category="secondary"
          (emitEvent)="openUploadPhotoModal.emit('Profile')"
        ></shared-button>
        } @else {
        <shared-button
          class="add-post"
          content="Add Post"
          (emitEvent)="addPost.emit('add')"
        ></shared-button>
        <shared-button
          class="edit-profile"
          content="Edit Profile"
          category="secondary"
          [routerLink]="['/authorized/user-profile/settings']"
        ></shared-button>
        }
      </div>
    </div>
  `,
  styleUrl: './avatar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AvatarComponent {
  isSettingsSection = input.required<boolean>();
  profilePhotoUrl = input.required<string>();
  backgroundPhotoUrl = input.required<string>();
  selectedOption = output();
  openUploadPhotoModal = output<'Profile' | 'Background'>();
  addPost = output<string>();

  defaultUserPhoto = signal('global/assets/default-user-photo.png');
}
