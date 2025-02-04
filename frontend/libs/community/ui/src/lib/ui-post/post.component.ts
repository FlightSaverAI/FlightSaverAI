import { ChangeDetectionStrategy, Component, input, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { CommentComponent } from '../ui-comment/comment.component';
import { DropdownDirective } from '@shared/ui-components';

@Component({
  selector: 'community-post',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage, CommentComponent, DropdownDirective],
  template: `<div class="post">
    <div class="post__user">
      <div class="u-flex u-gap-1">
        <img
          class="post__user-photo"
          [ngSrc]="user().profilePictureUrl"
          alt="user-photo"
          width="50"
          height="50"
        />
        <div class="post__user-info">
          <span class="post__user-name">{{ user().username }}</span>
          <span class="post__user-location"
            >{{ post().postedAt | date: 'dd MMM yyyy, HH:mm:ss' }}, {{ post().location }}
          </span>
        </div>
      </div>
      <img
        class="post__more-options"
        ngSrc="global/assets/assets-community/more-options.svg"
        alt="user-photo"
        width="40"
        height="40"
        sharedDropdown
        [dropdownConfig]="dropdownConfig()"
        (selectOption)="selectedDropdownOption.emit($event)"
      />
    </div>
    <div class="post__content-container">
      <p
        class="post__content"
        [ngStyle]="post().imageUrl ? { paddingBottom: '15px' } : { padding: '15px 0' }"
      >
        {{ post().content }}
      </p>
      @if(post().imageUrl){
      <img class="post__content-image u-w-100" [src]="post().imageUrl" alt="" />
      }
    </div>
    <div class="post__interaction">
      <div class="post__interaction-likes">
        <img
          ngSrc="global/assets/assets-community/like.svg"
          alt="Like icon"
          width="25"
          height="25"
        />
        <p>{{ post().likesCount }} likes</p>
      </div>
      <div class="post__interaction-comments" (click)="toggleCommentSection()">
        <img
          ngSrc="global/assets/assets-community/comment.svg"
          alt="Comment icon"
          width="25"
          height="25"
        />
        <span>{{ post().commentsCount }} comments</span>
      </div>
    </div>
    @if(isCommentSectionOpen()){
    <div class="post__comments-list">
      @for(comment of comments(); track comment){
      <community-comment class="comment" [comment]="comment"></community-comment>
      }

      <div class="container">
        <img [ngSrc]="user().profilePictureUrl" alt="" width="40" height="40" />
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
  post = input<any>();
  dropdownConfig = input<any>();
  comments = input.required<any>();
  selectedDropdownOption = output<string>();

  isCommentSectionOpen = signal(false);

  protected toggleCommentSection() {
    this.isCommentSectionOpen.update((state) => !state);
  }
}
