import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import { AvatarComponent, ManagePostModalComponent } from '@flight-saver/user-profile/ui';
import { ConfirmModalComponent, FlightsSummaryComponent } from '@shared/ui';
import { toSignal } from '@angular/core/rxjs-interop';
import { LikesAndCommentsService, PostService } from '@flight-saver/user-profile/data-access';
import { AlertService, UserService } from '@shared/data-access';
import { MatDialog } from '@angular/material/dialog';
import { PostConstants } from '../constants/post.constants';
import { filter, switchMap, tap } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
  standalone: true,
  imports: [CommonModule, PostComponent, AvatarComponent, FlightsSummaryComponent],
  template: ` <div class="travel">
    @defer(when userData() && basicStatistics()){
    <user-profile-avatar
      [profilePhotoUrl]="userData().profilePictureUrl"
      [backgroundPhotoUrl]="userData().backgroundPictureUrl"
      [username]="userData().username"
      [isSettingsSection]="false"
      (addPost)="openManagePostModal($event)"
    ></user-profile-avatar>
    <shared-flights-summary
      [statistics]="basicStatistics()"
      [isAdvanced]="false"
    ></shared-flights-summary>
    <div class="posts-container">
      @for(post of userPosts(); track post.id){
      <community-post
        class="u-justify-center u-w-100"
        [user]="userData()"
        [post]="post"
        [comments]="postComments()[post.id] || []"
        [commentFormControl]="commentFormControl()"
        [dropdownConfig]="dropdownConfig()"
        [currentUserProfilePicture]="userData().profilePictureUrl"
        (selectedDropdownOption)="openManagePostModal($event, post)"
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
    }
  </div>`,
  styleUrl: './user-profile-container.component.scss',
  providers: [PostService, LikesAndCommentsService],
})
export class UserProfileContainerComponent implements OnInit {
  private _dialog = inject(MatDialog);
  private _userService = inject(UserService);
  private _postService = inject(PostService);
  private _likesAndCommentsService = inject(LikesAndCommentsService);
  private _countriesOptions = PostConstants.countries;
  private _alertService = inject(AlertService);

  //TO FIX (this endpoint should be in shared data access library)
  protected userData = toSignal(this._userService.getUserProfileData());
  protected basicStatistics = toSignal(this._userService.getUserStatisticsPreview(null));
  protected dropdownConfig = signal(PostConstants.dropdownConfig);
  protected selectedPostId: any = null;
  protected userPosts: any = signal([]);
  protected postComments = signal<Record<string, any[]>>({});
  protected commentFormControl = signal(new FormControl(''));
  protected editMode = signal({ state: false, postId: '', commentId: '' });

  private _postModalConfigs: any = {
    add: {
      component: ManagePostModalComponent,
      countries: this._countriesOptions,
      title: 'Add Post',
      width: '850px',
      callback: (form: any) => this._postService.addPost(form),
    },
    edit: {
      component: ManagePostModalComponent,
      countries: this._countriesOptions,
      title: 'Edit Post',
      width: '850px',
      callback: (updatedPost: any) => this._postService.managePostEdition(updatedPost),
    },
    delete: {
      component: ConfirmModalComponent,
      content: 'Are you sure you want to delete this post ?',
      title: 'Delete Post',
      width: '500px',
      callback: () => this._postService.deletePost(this.selectedPostId),
    },
  };

  public ngOnInit() {
    this._getPosts();
  }

  protected openManagePostModal(operationType: string, post?: any) {
    const { width, callback, ...data } = this._postModalConfigs[operationType.toLowerCase()];

    if (post) {
      this.selectedPostId = post.id;
    }

    const modalRef = this._dialog.open(data.component, {
      data: { ...data, post: post ? post : undefined },
      width,
    });

    modalRef
      .afterClosed()
      .pipe(
        filter(Boolean),
        switchMap((form) => (callback.length > 0 ? callback(form) : callback(post.id))),
        tap(() => this._getPosts()),
        tap(() => (this.selectedPostId = null))
      )
      .subscribe();
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
    this._postService.getUserPosts().subscribe((posts) => {
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
