import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import { AvatarComponent } from '@flight-saver/user-profile/ui';

@Component({
  standalone: true,
  imports: [CommonModule, PostComponent, AvatarComponent],
  template: `<div class="travel">
    <user-profile-avatar></user-profile-avatar>
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
  </div>`,
  styleUrl: './user-profile-container.component.scss',
})
export class UserProfileContainerComponent {
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
