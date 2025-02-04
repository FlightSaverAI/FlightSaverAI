import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import { AvatarComponent, ManagePostModalComponent } from '@flight-saver/user-profile/ui';
import { ConfirmModalComponent, FlightsSummaryComponent } from '@shared/ui';
import { toSignal } from '@angular/core/rxjs-interop';
import { HomeService } from '@flight-saver/home/data-access';
import { PostService, UserProfileService } from '@flight-saver/user-profile/data-access';
import { MatDialog } from '@angular/material/dialog';
import { PostConstants } from '../constants/post.constants';
import { filter, switchMap, tap } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, PostComponent, AvatarComponent, FlightsSummaryComponent],
  template: ` <div class="travel">
    @defer(when userData()){
    <user-profile-avatar
      [profilePhotoUrl]="userData().profilePictureUrl"
      [backgroundPhotoUrl]="userData().backgroundPictureUrl"
      [isSettingsSection]="false"
      (addPost)="openManagePostModal($event)"
    ></user-profile-avatar>
    <shared-flights-summary
      [statistics]="basicStatistics()"
      [isAdvanced]="false"
    ></shared-flights-summary>
    <div class="posts-container">
      @for(post of userPosts | async; track post){
      <community-post
        class="u-justify-center u-w-100"
        [user]="userData()"
        [post]="post"
        [comments]="post.commentsList"
        [dropdownConfig]="dropdownConfig()"
        (selectedDropdownOption)="openManagePostModal($event, post)"
      ></community-post>
      }
    </div>
    }
  </div>`,
  styleUrl: './user-profile-container.component.scss',
  providers: [UserProfileService, PostService],
})
export class UserProfileContainerComponent {
  private _dialog = inject(MatDialog);
  private _userProfileService = inject(UserProfileService);
  private _postService = inject(PostService);
  private _countriesOptions = PostConstants.countries;

  //TO FIX (this endpoint should be in shared data access library)
  protected userData = toSignal(this._userProfileService.getUserProfileData());
  protected userPosts = this._postService.getUserPosts();
  protected basicStatistics = toSignal(inject(HomeService).getBasicStatistics());
  protected dropdownConfig = signal(PostConstants.dropdownConfig);
  protected selectedPostId: any = null;

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
      callback: (updatedPost: any) => this._postService.editPost(updatedPost, this.selectedPostId),
    },
    delete: {
      component: ConfirmModalComponent,
      content: 'Are you sure you want to delete this post ?',
      title: 'Delete Post',
      width: '500px',
      callback: () => this._postService.deletePost(this.selectedPostId),
    },
  };

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
        switchMap((form) => (callback.length > 0 ? callback(form) : callback())),
        tap(() => (this.userPosts = this._postService.getUserPosts())),
        tap(() => (this.selectedPostId = null))
      )
      .subscribe();
  }
}
