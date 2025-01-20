import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FlightFormService {
  private _http = inject(HttpClient);

  getAirports(): Observable<any> {
    return this._http.get(`${environment.url}/airports/minimal`);
  }

  getAirlines(): Observable<any> {
    return this._http.get(`${environment.url}/airlines/minimal`);
  }

  getAircrafts(): Observable<any> {
    return this._http.get(`${environment.url}/aircraft/minimal`);
  }

  addFlight(payload: any): Observable<any> {
    return this._http.post(`${environment.url}/flights`, payload);
  }
}
