import { Component, computed, input, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormatTimePipe } from '@shared/ui';
import { NgOptimizedImage } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'friends-card',
  standalone: true,
  imports: [CommonModule, FormatTimePipe, NgOptimizedImage, ButtonComponent],
  template: ` <div class="card">
    <div class="card__image-container">
      <img
        class="card__image"
        [src]="friend().profilePictureUrl || defaultUserPhoto()"
        alt="User photo"
      />
      <div class="card__title">
        <p>{{ friend().name }}</p>
      </div>
    </div>
    <div class="card__info-container">
      <div class="card__stats-container">
        <div class="card__stats">
          <img ngSrc="global/assets/time.svg" width="25" height="25" alt="" />
          <p>
            <strong>{{ friend().statistics.flightCount.count }}</strong> flights
          </p>
        </div>
        <div class="card__stats">
          <img ngSrc="global/assets/flight.svg" width="25" height="25" alt="" />
          <p>
            <strong>{{ friend().statistics.totalFlightTime.time | formatTime }}</strong>
          </p>
        </div>
        <div class="card__stats">
          <img ngSrc="global/assets/distance.svg" width="25" height="25" alt="" />
          <p>
            <strong>{{ totalDistance() }}</strong> km
          </p>
        </div>
      </div>
      <div class="card__info-btn-container">
        <shared-button content="View Wall"></shared-button>
        <shared-button
          [content]="friend().isLoggedUserFriend ? '✓ Friends' : '+ Add Friend'"
          (emitEvent)="
            friend().isLoggedUserFriend
              ? removeFriend.emit(friend().id)
              : addFriend.emit(friend().id)
          "
        ></shared-button>
      </div>
    </div>
  </div>`,
  styleUrl: './friends-card.component.scss',
})
export class FriendsCardComponent {
  friend = input.required<any>();
  totalDistance = computed(() => Math.round(this.friend().statistics.distance.totalDistance));

  addFriend = output<string>();
  removeFriend = output<string>();

  defaultUserPhoto = signal('global/assets/default-user-photo.png');
}
