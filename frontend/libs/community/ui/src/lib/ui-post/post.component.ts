import { ChangeDetectionStrategy, Component, input, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { CommentComponent } from '../ui-comment/comment.component';
import { DropdownDirective } from '@shared/ui-components';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'community-post',
  standalone: true,
  imports: [
    CommonModule,
    NgOptimizedImage,
    CommentComponent,
    DropdownDirective,
    ReactiveFormsModule,
  ],
  template: `<div class="post">
    <div class="post__user">
      <div class="u-flex u-gap-1">
        <img
          class="post__user-photo"
          [ngSrc]="user().profilePictureUrl || defaultUserPhoto()"
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
      @if(dropdownConfig()){
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
      }
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
        <div
          class="like-icon"
          [ngClass]="post().isLikedByCurrentUser ? 'active' : ''"
          (click)="toggleActive(post().id, post().isLikedByCurrentUser)"
        ></div>
        <p>{{ post().likesCount }} likes</p>
      </div>
      <div class="post__interaction-comments" (click)="toggleCommentSection(post().id)">
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
      @if(comments()?.[0]?.socialPostId === post().id){ @for(comment of comments(); track
      comment.id){
      <community-comment class="comment" [comment]="comment"></community-comment>
      } }
      <div class="container">
        <img
          [ngSrc]="currentUserProfilePicture() || defaultUserPhoto()"
          alt=""
          width="40"
          height="40"
        />
        <div class="hehe">
          <textarea [formControl]="content" placeholder="Write the comment..."></textarea>
          <div class="icons">
            <img ngSrc="global/assets/assets-community/emoji.svg" alt="" width="15" height="15" />
            <img
              ngSrc="global/assets/assets-community/save.svg"
              alt=""
              width="15"
              height="15"
              (click)="saveComment(post().id)"
            />
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
  currentUserProfilePicture = input.required<string>();

  selectedDropdownOption = output<string>();
  loadComments = output<string>();
  addComment = output<any>();
  likePost = output<string>();
  unlikePost = output<string>();

  isCommentSectionOpen = signal(false);
  defaultUserPhoto = signal('global/assets/default-user-photo.png');
  content = new FormControl('');

  protected toggleActive(postId: string, isLikedByCurrentUser: boolean) {
    isLikedByCurrentUser ? this.unlikePost.emit(postId) : this.likePost.emit(postId);
  }

  protected toggleCommentSection(postId: string) {
    this.isCommentSectionOpen.update((state) => !state);

    if (this.isCommentSectionOpen()) {
      this.loadComments.emit(postId);
    }
  }

  protected saveComment(postId: string) {
    const content = this.content.value;
    this.addComment.emit({ postId, content });
  }
}
