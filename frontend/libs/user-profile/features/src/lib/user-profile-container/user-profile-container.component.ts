import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import { AvatarComponent } from '@flight-saver/user-profile/ui';
import { FlightsSummaryComponent } from '@shared/ui';
import { toSignal } from '@angular/core/rxjs-interop';
import { HomeService } from '@flight-saver/home/data-access';
import { SettingsService } from '@flight-saver/user-profile/data-access';

@Component({
  standalone: true,
  imports: [CommonModule, PostComponent, AvatarComponent, FlightsSummaryComponent],
  template: `<div class="travel">
    @defer(when userData()){
    <user-profile-avatar
      [profilePhotoUrl]="userData().profilePictureUrl"
      [backgroundPhotoUrl]="userData().backgroundPictureUrl"
      [isSettingsSection]="false"
    ></user-profile-avatar>
    <shared-flights-summary
      [statistics]="basicStatistics()"
      [isAdvanced]="false"
    ></shared-flights-summary>
    <div class="posts-container">
      @for(post of mockedUsersPosts; track post){
      <community-post
        [user]="post.user"
        [content]="post.content"
        [flightDetails]="post.content.flightDetails"
        [interactions]="post.interactions"
        [comments]="post.commentsList"
      ></community-post>
      }
    </div>
    }
  </div>`,
  styleUrl: './user-profile-container.component.scss',
  providers: [SettingsService],
})
export class UserProfileContainerComponent {
  //TO FIX (this endpoint should be in shared data access library)
  protected userData = toSignal(inject(SettingsService).getUserProfileData());
  protected basicStatistics = toSignal(inject(HomeService).getBasicStatistics());

  mockedUsersPosts = [
    {
      user: {
        name: 'Emily Johnson',
        photo: 'global/assets/images/user-photo.png',
        location: {
          city: 'Paris',
          country: 'France',
          date: 'July 20, 2024',
        },
      },
      content: {
        description:
          'Paris is magical! The Eiffel Tower at night is truly a sight to behold. Loved the croissants and coffee in quaint little cafes. Only downside was the long lines at some attractions, but totally worth it. 🗼🥐',
        flightDetails: {
          departure: {
            date: 'July 10, 2024',
            time: '09:00',
            location: 'LHR',
          },
          arrival: {
            time: '14:15',
            location: 'Thira',
          },
        },
        image: 'global/assets/images/city.png',
      },
      interactions: {
        likes: {
          count: 123,
          likedBy: 'Kaiya Curtis',
        },
        comments: {
          count: 5,
        },
      },
      commentsList: [],
    },
    {
      user: {
        name: 'Emily Johnson',
        photo: 'global/assets/images/user-photo.png',
        location: {
          city: 'Sydney',
          country: 'Australia',
          date: 'July 18, 2024',
        },
      },
      content: {
        description:
          'Sydney is fantastic! The Opera House is as impressive as I imagined. Loved the beaches and the vibrant city life. The long flight was tough, but the experience here makes it all worthwhile. 🏖️🎭',
        flightDetails: {
          departure: {
            date: 'July 17, 2024',
            time: '23:00',
            location: 'LAX',
          },
          arrival: {
            time: '03:30',
            location: 'NRT',
          },
        },
        image: 'global/assets/images/building.png',
      },
      interactions: {
        likes: {
          count: 123,
          likedBy: 'Natalia Brown',
        },
        comments: {
          count: 5,
        },
      },
      commentsList: [],
    },
  ];
}
