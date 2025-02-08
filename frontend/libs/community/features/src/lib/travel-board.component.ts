import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import {
  LikesAndCommentsService,
  PostService,
  UserProfileService,
} from '@flight-saver/user-profile/data-access';
import { toSignal } from '@angular/core/rxjs-interop';

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
      [comments]="postComments()[post.id] || []"
      (likePost)="likePost($event)"
      (unlikePost)="unlikePost($event)"
      (loadComments)="getPostComments($event)"
      (addComment)="addNewComment($event)"
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

  protected userPosts: any = signal([]);
  protected postComments = signal<Record<string, any[]>>({});
  protected userData = toSignal(this._userProfileService.getUserProfileData());

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

  public addNewComment({ postId, content }: any) {
    this._likesAndCommentsService.addComment(postId, content).subscribe(() => {
      this.getPostComments(postId);
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
