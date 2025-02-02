import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';

@Component({
  selector: 'community-travel-board',
  standalone: true,
  imports: [CommonModule, PostComponent],
  template: `<section class="travel-board">
    <!-- @for(post of mockedUsersPosts; track post){
    <community-post
      [user]="post.user"
      [content]="post.content"
      [flightDetails]="post.content.flightDetails"
      [interactions]="post.interactions"
      [comments]="post.commentsList"
    ></community-post>
    } -->
  </section>`,
  styleUrl: './travel-board.component.scss',
})
export class TravelBoardComponent {
  mockedUsersPosts = [
    {
      user: {
        name: 'Anna Smith',
        photo: 'global/assets/assets-community/user-photo-1.png',
        location: {
          city: 'Santorini',
          country: 'Greece',
          date: 'July 20, 2024',
        },
      },
      content: {
        description:
          'The views are breathtaking, white houses and blue domes – Santorini is stunning! However, the tourist crowds can be overwhelming. Despite this, every moment here is worth experiencing. 🏖✨ The sunsets here are truly magical, painting the sky with hues of orange and pink, making it the perfect place for unforgettable memories. 🌅💕',
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
        image: 'global/assets/assets-community/santorini.png',
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
    },
    {
      user: {
        name: 'Tom Green',
        photo: 'global/assets/assets-community/user-photo-2.png',
        location: {
          city: 'Tokoy',
          country: 'Japan',
          date: 'July 18, 2024',
        },
      },
      content: {
        description:
          'Tokyo is the city of the future! Technology and neon lights everywhere. I love the local food, especially sushi. The only downside is public transportation during rush hours – its incredibly crowded. 🍣🚄',
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
        image: 'global/assets/assets-community/tokyo.png',
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
    },
    {
      user: {
        name: 'Monica Taylor',
        photo: 'global/assets/assets-community/user-photo-3.png',
        location: {
          city: 'Rio De Janeiro',
          country: 'Brazil',
          date: 'July 15, 2024',
        },
      },
      content: {
        description:
          'Rio de Janeiro is full of energy and colors! The beaches are beautiful, and the people are incredibly friendly. Unfortunately, you need to be cautious in some parts of the city for safety reasons. But overall, its an amazing place to visit. 🌴🎉',
        flightDetails: {
          departure: {
            date: 'July 14, 2024',
            time: '18:15',
            location: 'JFK',
          },
          arrival: {
            time: '06:00',
            location: 'GIG',
          },
        },
        image: 'global/assets/assets-community/rio.png',
      },
      interactions: {
        likes: {
          count: 310,
          likedBy: 'Bart Watson',
        },
        comments: {
          count: 5,
        },
      },
      commentsList: [
        {
          userName: 'Natalie Brown',
          photo: 'global/assets/assets-community/user-photo-4.png',
          comment:
            'I am so jealous you are in Tokyo! Which sushi do you recommend trying there? 🍣',
          time: '3h',
        },
        {
          userName: 'Michael Davis',
          photo: 'global/assets/assets-community/user-photo-5.png',
          comment: ' Have you visited Shibuya Crossing yet? It must be an amazing experience!',
          time: '1h',
        },
      ],
    },
  ];
}
