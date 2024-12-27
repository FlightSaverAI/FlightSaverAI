import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'user-profile-avatar',
  standalone: true,
  imports: [CommonModule, ButtonComponent],
  template: ` <div class="container">
      <img src="global/assets/background-image.png" alt="" />
      <div class="avatar">
        <img src="global/assets/images/avatar.png" alt="" />
        <shared-button class="add-post" content="Add Post"></shared-button>
        <shared-button
          class="edit-profile"
          content="Edit Profile"
          category="secondary"
        ></shared-button>
      </div>
    </div>
    <div class="wrapper">
      <div class="summary">
        <div class="summary__flights">
          <img src="global/assets/flight.svg" alt="" />
          <div class="summary__stats">
            <p><strong>12</strong> flights</p>
          </div>
        </div>
        <div class="summary__flights">
          <img src="global/assets/distance.svg" alt="" />
          <div class="summary__stats">
            <p><strong>5670</strong> km</p>
          </div>
        </div>
        <div class="summary__flights">
          <img src="global/assets/time.svg" alt="" />
          <div class="summary__stats">
            <p><strong>29h 35 min</strong></p>
          </div>
        </div>
      </div>
    </div>`,
  styleUrl: './avatar.component.scss',
})
export class AvatarComponent {}
