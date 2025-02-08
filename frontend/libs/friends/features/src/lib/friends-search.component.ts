import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputSearchComponent } from '@shared/ui-components';
import { FriendsService } from '@flight-saver/friends/data-access';
import { FriendCardComponent } from '@flight-saver/friends/ui';
import { NgOptimizedImage } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule, InputSearchComponent, FriendCardComponent, NgOptimizedImage],
  template: ` <div class="friends-board">
    <shared-input-search
      placeholder="Seach new friends"
      iconSrc="global/assets/assets-community/search.svg"
      (valueChangeListener)="searchFriends($event)"
    ></shared-input-search>
    <div class="friends-container">
      @if(users().length !== 0){ @for(friend of users(); track friend.id){
      <friend-card [friend]="friend" (addFriend)="addFriend($event)"></friend-card>
      } } @else{
      <div class="no-user-found">
        <img
          ngSrc="global/assets/assets-community/user-not-found.svg"
          width="100"
          height="100"
          alt="No user found"
        />
        <p>No friends found</p>
      </div>
      }
    </div>
  </div>`,
  styleUrl: './friends-search.component.scss',
  providers: [FriendsService],
})
export class FriendsSearchComponent {
  private _friendsService = inject(FriendsService);

  private _currentSearchQuery = '';
  protected users: any = signal([]);

  protected addFriend(friendId: string) {
    this._friendsService.addFriend(friendId).subscribe(() => {
      this.searchFriends(this._currentSearchQuery);
    });
  }

  protected searchFriends(query: string) {
    this._currentSearchQuery = query;

    this._friendsService.searchFriends(query).subscribe(({ users }) => {
      this.users.set(users);
    });
  }
}
