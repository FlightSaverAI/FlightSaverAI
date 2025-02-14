import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private _http = inject(HttpClient);
  public userPhoto = new BehaviorSubject<string | null>(null);

  public getUserProfileData(userId?: any | null) {
    return this._http.get<any>(`${environment.url}/user/info`, {
      params: userId ? { userId } : {},
    });
  }

  public getUserStatisticsPreview(userId: string | null) {
    return this._http.get(`${environment.url}/statistic/basic`, {
      params: userId ? { userId } : {},
    });
  }
}
