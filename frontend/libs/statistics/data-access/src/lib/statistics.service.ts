import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  private _httpClient = inject(HttpClient);

  public getTopOverview(): Observable<any> {
    return this._httpClient.get(`${environment.url}/statistics/bar`);
  }

  public getFlightPassangerOverview(): Observable<any> {
    return this._httpClient.get(`${environment.url}/statistics/circual`);
  }

  public getActivityOverview(): Observable<any> {
    return this._httpClient.get(`${environment.url}/statistics/line`);
  }
}
