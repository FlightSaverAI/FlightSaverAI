import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import { AddPostModalComponent, AvatarComponent } from '@flight-saver/user-profile/ui';
import { FlightsSummaryComponent } from '@shared/ui';
import { toSignal } from '@angular/core/rxjs-interop';
import { HomeService } from '@flight-saver/home/data-access';
import { UserProfileService } from '@flight-saver/user-profile/data-access';
import { MatDialog } from '@angular/material/dialog';
import { AddPostConstants } from '../constants/add-post.constants';
import { filter, switchMap, tap } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, PostComponent, AvatarComponent, FlightsSummaryComponent],
  template: `<div class="travel">
    @defer(when userData()){
    <user-profile-avatar
      [profilePhotoUrl]="userData().profilePictureUrl"
      [backgroundPhotoUrl]="userData().backgroundPictureUrl"
      [isSettingsSection]="false"
      (openAddPostModal)="openAddPostModal()"
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
      ></community-post>
      }
    </div>
    }
  </div>`,
  styleUrl: './user-profile-container.component.scss',
  providers: [UserProfileService],
})
export class UserProfileContainerComponent {
  private _dialog = inject(MatDialog);
  private _userProfileService = inject(UserProfileService);

  //TO FIX (this endpoint should be in shared data access library)
  protected userData = toSignal(this._userProfileService.getUserProfileData());
  protected userPosts = this._userProfileService.getUserPosts();
  protected basicStatistics = toSignal(inject(HomeService).getBasicStatistics());

  protected openAddPostModal() {
    const modalRef = this._dialog.open(AddPostModalComponent, {
      data: {
        countries: AddPostConstants.countries,
      },
      width: '850px',
    });

    modalRef
      .afterClosed()
      .pipe(
        filter(Boolean),
        switchMap((form) => this._userProfileService.addPost(form)),
        tap(() => (this.userPosts = this._userProfileService.getUserPosts()))
      )
      .subscribe();
  }
}
