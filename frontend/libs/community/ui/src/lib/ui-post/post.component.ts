import { ChangeDetectionStrategy, Component, input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { CommentComponent } from '../ui-comment/comment.component';

@Component({
  selector: 'community-post',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage, CommentComponent],
  template: `<div class="post">
    <div class="post__user">
      <img
        class="post__user-photo"
        [ngSrc]="user().photo"
        alt="user-photo"
        width="50"
        height="50"
      />
      <div class="post__user-info">
        <span class="post__user-name">{{ user().name }}</span>
        <span class="post__user-location"
          >{{ user().location.date }}, {{ user().location.city }},
          {{ user().location.country }}</span
        >
      </div>
    </div>
    <div class="post__content">
      <p class="post__content-description">{{ content().description }}</p>
      <div class="post__content-flight-details">
        <p>
          Flight:
          <span
            >{{ flightDetails().departure.date }}, {{ flightDetails().departure.location }} ({{
              flightDetails().departure.time
            }})</span
          >
          ->
          <span>{{ flightDetails().arrival.location }} ({{ flightDetails().arrival.time }})</span>
        </p>
      </div>
      <img class="post__content-image u-w-100" [src]="content().image" alt="" />
    </div>
    <div class="post__interaction">
      <div class="post__interaction-likes">
        <img src="global/assets/assets-community/like.svg" alt="Like icon" width="25" height="25" />
        <p>
          Liked by {{ interactions().likes.likedBy }} and
          <span>{{ interactions().likes.count }} others</span>
        </p>
      </div>
      <div class="post__interaction-comments" (click)="toggleCommentSection()">
        <img
          ngSrc="global/assets/assets-community/comment.svg"
          alt="Comment icon"
          width="25"
          height="25"
        />
        <span>{{ interactions().comments.count }} comments</span>
      </div>
    </div>
    @if(isCommentSectionOpen()){
    <div class="post__comments-list">
      @for(comment of comments(); track comment){
      <community-comment class="comment" [comment]="comment"></community-comment>
      }

      <div class="container">
        <img src="global/assets/images/user-photo.png" alt="" width="40" height="40" />
        <div class="hehe">
          <textarea name="" id="" placeholder="Write the comment..."></textarea>
          <div class="icons">
            <img ngSrc="global/assets/assets-community/photo.svg" alt="" width="15" height="15" />
            <img ngSrc="global/assets/assets-community/emoji.svg" alt="" width="15" height="15" />
            <img ngSrc="global/assets/assets-community/save.svg" alt="" width="15" height="15" />
          </div>
        </div>
      </div>
    </div>
    }
  </div>`,
  styleUrl: './post.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PostComponent {
  user = input.required<any>();
  content = input.required<any>();
  flightDetails = input.required<any>();
  interactions = input.required<any>();
  comments = input.required<any>();

  isCommentSectionOpen = signal(false);

  toggleCommentSection() {
    this.isCommentSectionOpen.update((state) => !state);
  }
}
