import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';
import { Flight } from '@flight-saver/home/models';

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  private _http = inject(HttpClient);

  public getFlights() {
    return this._http.get<Flight[]>(`${environment.url}/flights/user/minimal`);
  }
}
