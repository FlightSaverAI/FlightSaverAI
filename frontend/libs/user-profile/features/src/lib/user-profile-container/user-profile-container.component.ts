import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from '@flight-saver/community/ui';
import { AvatarComponent } from '@flight-saver/user-profile/ui';
import { FlightsSummaryComponent } from '@shared/ui';
import { toSignal } from '@angular/core/rxjs-interop';
import { HomeService } from '@flight-saver/home/data-access';
import { UserProfileService } from '@flight-saver/user-profile/data-access';

@Component({
  standalone: true,
  imports: [CommonModule, PostComponent, AvatarComponent, FlightsSummaryComponent],
  template: `<div class="travel">
    @defer(when userData()){
    <user-profile-avatar
      [profilePhotoUrl]="userData().profilePictureUrl"
      [backgroundPhotoUrl]="userData().backgroundPictureUrl"
      [isSettingsSection]="false"
    ></user-profile-avatar>
    <shared-flights-summary
      [statistics]="basicStatistics()"
      [isAdvanced]="false"
    ></shared-flights-summary>
    <div class="posts-container">
      @for(post of userPosts(); track post){
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
  //TO FIX (this endpoint should be in shared data access library)
  protected userData = toSignal(inject(UserProfileService).getUserProfileData());
  protected basicStatistics = toSignal(inject(HomeService).getBasicStatistics());
  protected userPosts = toSignal(inject(UserProfileService).getUserPosts());
}
