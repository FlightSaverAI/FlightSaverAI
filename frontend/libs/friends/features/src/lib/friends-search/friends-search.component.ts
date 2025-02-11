import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputSearchComponent } from '@shared/ui-components';
import { FriendsService } from '@flight-saver/friends/data-access';
import { FriendsCardComponent } from '@flight-saver/friends/ui';
import { NoUserFoundComponent } from '@flight-saver/friends/ui';
import { AlertService } from '@shared/data-access';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  imports: [CommonModule, InputSearchComponent, FriendsCardComponent, NoUserFoundComponent],
  template: ` <div class="friends-board">
    <shared-input-search
      placeholder="Seach new friends"
      iconSrc="global/assets/assets-community/search.svg"
      (valueChangeListener)="searchFriends($event)"
    ></shared-input-search>
    <p>Search results: {{ users().length }}</p>
    <div class="friends-container">
      <!-- prettier-ignore -->
      @if(users().length > 0){ 
        @for(friend of users(); track friend.id){
      <friends-card
        [friend]="friend"
        (addFriend)="addFriend($event)"
        (removeFriend)="removeFriend($event)"
        (goToUserWall)="goToUserProfile($event)"
      ></friends-card>
      } } @else{
      <friends-no-user-found class="u-w-100"></friends-no-user-found>
      }
    </div>
  </div>`,
  styleUrl: './friends-search.component.scss',
  providers: [FriendsService],
})
export class FriendsSearchComponent {
  private _friendsService = inject(FriendsService);
  private _alertService = inject(AlertService);
  private _router = inject(Router);

  private _currentSearchQuery = '';
  protected users: any = signal([]);

  protected addFriend(friendId: string) {
    this._friendsService.addFriend(friendId).subscribe({
      next: () => this._alertService.showAlert('success', 'Friend added successfully'),
      error: () => this._alertService.showAlert('error', 'Error adding friend'),
      complete: () => this.searchFriends(this._currentSearchQuery),
    });
  }

  protected removeFriend(friendId: string) {
    this._friendsService.removeFriend(friendId).subscribe({
      next: () => this._alertService.showAlert('success', 'Friend removed successfully'),
      error: () => this._alertService.showAlert('error', 'Error removing friend'),
      complete: () => this.searchFriends(this._currentSearchQuery),
    });
  }

  protected searchFriends(query: string) {
    this._currentSearchQuery = query;

    this._friendsService.searchFriends(query).subscribe(({ users }) => {
      this.users.set(users);
    });
  }

  protected goToUserProfile(friendId: any) {
    this._router.navigate(['authorized/users-search', friendId]);
  }
}
