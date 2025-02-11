import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  private _http = inject(HttpClient);

  public getFlights(): Observable<any> {
    return this._http.get(`${environment.url}/flights/user/minimal`);
  }

  public getBasicStatistics(userId?: any): Observable<any> {
    return this._http.get(`${environment.url}/statistic/basic`, {
      params: userId ? { userId } : {},
    });
  }
}
