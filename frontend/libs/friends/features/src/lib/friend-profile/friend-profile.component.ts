//TO Refactor (this component should be reusable)
import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  LikesAndCommentsService,
  PostService,
  UserProfileService,
} from '@flight-saver/user-profile/data-access';
import { PostComponent } from '@flight-saver/community/ui';
import { AvatarComponent } from '@flight-saver/user-profile/ui';
import { FlightsSummaryComponent } from '@shared/ui';
import { NgOptimizedImage } from '@angular/common';
import { AlertService, UserService } from '@shared/data-access';
import { FormControl } from '@angular/forms';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    PostComponent,
    AvatarComponent,
    FlightsSummaryComponent,
    NgOptimizedImage,
  ],
  template: ` @defer(when userData() && basicStatistics()){
    <div class="travel">
      <user-profile-avatar
        [profilePhotoUrl]="userData().profilePictureUrl"
        [backgroundPhotoUrl]="userData().backgroundPictureUrl"
        [isSettingsSection]="false"
        [isFriendsSection]="true"
        [username]="userData().username"
      ></user-profile-avatar>
      <shared-flights-summary
        [statistics]="basicStatistics()"
        [isAdvanced]="false"
      ></shared-flights-summary>
      @if(userPosts().length > 0){
      <div class="posts-container">
        @for(post of userPosts(); track post.id){
        <community-post
          class="u-justify-center u-w-100"
          [user]="userData()"
          [post]="post"
          [currentUserProfilePicture]="currentUser().profilePictureUrl"
          [commentFormControl]="commentFormControl()"
          [comments]="postComments()[post.id] || []"
          (likePost)="likePost($event)"
          (unlikePost)="unlikePost($event)"
          (loadComments)="getPostComments($event)"
          [(editMode)]="editMode"
          (addComment)="addNewComment($event)"
          (updateComment)="updateNewComment($event)"
          (deleteComment)="deleteComment($event)"
        ></community-post>
        }
      </div>
      }@else {
      <div class="no-post-found">
        <img
          ngSrc="global/assets/assets-community/no-result-found-icon.svg"
          width="75"
          height="75"
          alt="No user found"
        />
        <p>No posts found</p>
      </div>
      }
    </div>
    }`,
  styleUrl: './friend-profile.component.scss',
  providers: [UserProfileService, PostService, LikesAndCommentsService],
})
export class FriendProfileComponent implements OnInit {
  private _selectedUserId = inject(ActivatedRoute).snapshot.paramMap.get('id');
  private _userService = inject(UserService);
  private _postService = inject(PostService);
  private _likesAndCommentsService = inject(LikesAndCommentsService);
  private _alertService = inject(AlertService);

  protected userData = toSignal(this._userService.getUserProfileData(this._selectedUserId));
  protected currentUser = toSignal(this._userService.getUserProfileData());
  protected commentFormControl = signal(new FormControl(''));
  protected editMode = signal({ state: false, postId: '', commentId: '' });

  protected basicStatistics = toSignal(
    this._userService.getUserStatisticsPreview(this._selectedUserId)
  );

  protected userPosts: any = signal([]);
  protected postComments = signal<Record<string, any[]>>({});

  public ngOnInit() {
    this._getPosts();
  }

  protected likePost(postId: string) {
    this._likesAndCommentsService.likePost(postId).subscribe(() => {
      this._getPosts();
    });
  }

  protected unlikePost(postId: string) {
    this._likesAndCommentsService.unlikePost(postId).subscribe(() => {
      this._getPosts();
    });
  }

  private _getPosts() {
    this._postService.getUserPosts(this._selectedUserId).subscribe((posts) => {
      this.userPosts.set(posts);
    });
  }

  protected addNewComment({ postId, content }: any) {
    this._likesAndCommentsService.addComment(postId, content).subscribe({
      next: () => {
        this.getPostComments(postId);
        this._getPosts();
        this._alertService.showAlert('success', 'Comment added successfully');
      },
      error: () => {
        this._alertService.showAlert('error', 'Failed to add comment');
      },
      complete: () => {
        this.commentFormControl().reset();
      },
    });
  }

  protected updateNewComment({ content }: any) {
    this._likesAndCommentsService.updateComment(this.editMode().commentId, content).subscribe({
      next: () => {
        this.getPostComments(this.editMode().postId);
        this._alertService.showAlert('success', 'Comment updated successfully');
      },
      error: () => {
        this._alertService.showAlert('error', 'Failed to update comment');
      },
      complete: () => {
        this.commentFormControl().reset();
        this.editMode.update(() => ({ state: false, postId: '', commentId: '' }));
      },
    });
  }

  protected deleteComment({ commentId, postId }: any) {
    this._likesAndCommentsService.deleteComment(commentId).subscribe({
      next: () => {
        this.getPostComments(postId);
        this._getPosts();
        this._alertService.showAlert('success', 'Comment deleted successfully');
      },
      error: () => {
        this._alertService.showAlert('error', 'Failed to delete comment');
      },
    });
  }

  protected getPostComments(postId: string) {
    this._likesAndCommentsService.getPostComments(postId).subscribe((comments) => {
      this.postComments.update((prevComments) => ({
        ...prevComments,
        [postId]: comments,
      }));
    });
  }
}
