import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';
import { Observable } from 'rxjs';

@Injectable()
export class FriendsService {
  private _http = inject(HttpClient);

  public searchFriends(searchQuery: string): Observable<any> {
    return this._http.get(`${environment.url}/user`, {
      params: {
        name: searchQuery,
      },
    });
  }

  public addFriend(friendId: string): Observable<any> {
    return this._http.post(`${environment.url}/friend/add?friendId=${friendId}`, null);
  }

  public getAllFriends(): Observable<any> {
    return this._http.get(`${environment.url}/friend`);
  }
}
