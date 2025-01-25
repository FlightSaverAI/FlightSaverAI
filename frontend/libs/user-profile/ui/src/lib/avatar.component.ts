import { Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'user-profile-avatar',
  standalone: true,
  imports: [CommonModule, ButtonComponent],
  template: `
    <div class="container">
      <img src="global/assets/background-image.png" alt="" />
      <div class="avatar">
        <img src="global/assets/images/avatar.png" alt="" />
        @if(isSettingsSection()) {
        <shared-button
          class="update-picture"
          content="Edit Picture"
          category="secondary"
        ></shared-button>
        } @else {
        <shared-button class="add-post" content="Add Post"></shared-button>
        <shared-button
          class="edit-profile"
          content="Edit Profile"
          category="secondary"
        ></shared-button>
        }
      </div>
    </div>
  `,
  styleUrl: './avatar.component.scss',
})
export class AvatarComponent {
  isSettingsSection = input(false);
  selectedOption = output();
}
