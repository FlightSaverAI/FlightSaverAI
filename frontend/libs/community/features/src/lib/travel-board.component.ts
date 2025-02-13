import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import {
  LikesAndCommentsService,
  PostService,
  UserProfileService,
} from '@flight-saver/user-profile/data-access';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl } from '@angular/forms';
import { AlertService } from '@shared/data-access';

@Component({
  selector: 'community-travel-board',
  standalone: true,
  imports: [CommonModule, PostComponent],
  template: `<section class="travel-board">
    @for(post of userPosts(); track post){
    <community-post
      class="u-justify-center u-w-100"
      [user]="post.user"
      [post]="post"
      [currentUserProfilePicture]="userData().profilePictureUrl"
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
  </section>`,
  styleUrl: './travel-board.component.scss',
  providers: [UserProfileService, PostService, LikesAndCommentsService],
})
export class TravelBoardComponent implements OnInit {
  private _postService = inject(PostService);
  private _likesAndCommentsService = inject(LikesAndCommentsService);
  private _userProfileService = inject(UserProfileService);
  private _alertService = inject(AlertService);

  protected userPosts: any = signal([]);
  protected commentFormControl = signal(new FormControl(''));
  protected postComments = signal<Record<string, any[]>>({});
  protected userData = toSignal(this._userProfileService.getUserProfileData());
  protected editMode = signal({ state: false, postId: '', commentId: '' });

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
    this._postService.getAllPosts().subscribe((posts) => {
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

  public getPostComments(postId: string) {
    this._likesAndCommentsService.getPostComments(postId).subscribe((comments) => {
      this.postComments.update((prevComments) => ({
        ...prevComments,
        [postId]: comments,
      }));
    });
  }
}
