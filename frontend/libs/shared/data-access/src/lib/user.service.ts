import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private _http = inject(HttpClient);

  public getUserStatisticsPreview(userId: string | null) {
    return this._http.get(`${environment.url}/statistic/basic`, {
      params: userId ? { userId } : {},
    });
  }
}
