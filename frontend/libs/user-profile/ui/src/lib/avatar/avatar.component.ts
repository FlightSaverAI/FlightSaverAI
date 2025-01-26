import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'user-profile-avatar',
  standalone: true,
  imports: [CommonModule, ButtonComponent, RouterModule],
  template: `
    <div class="container">
      <img src="global/assets/background-image.png" alt="" />
      <div class="avatar">
        <img src="global/assets/images/avatar.png" alt="" />
        @if(isSettingsSection()) {
        <shared-button
          class="update-picture"
          content="Update Photo"
          category="secondary"
        ></shared-button>
        } @else {
        <shared-button class="add-post" content="Add Post"></shared-button>
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
  selectedOption = output();
}
