import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';
import {
  ActivityOverviewStatistics,
  FlightPassengerOverviewStatistics,
  TopOverviewStatistics,
} from '@flight-saver/statistics/models';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  private _httpClient = inject(HttpClient);

  public getTopOverview() {
    return this._httpClient.get<TopOverviewStatistics>(`${environment.url}/statistic/bar`);
  }

  public getFlightPassangerOverview() {
    return this._httpClient.get<FlightPassengerOverviewStatistics>(
      `${environment.url}/statistic/circual`
    );
  }

  public getActivityOverview() {
    return this._httpClient.get<ActivityOverviewStatistics>(`${environment.url}/statistic/line`);
  }
}
