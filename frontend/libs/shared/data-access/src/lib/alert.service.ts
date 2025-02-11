import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

type AlertType = 'success' | 'warning' | 'error';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  private _alertSubject = new BehaviorSubject<any | null>(null);
  public alert$ = this._alertSubject.asObservable();

  public showAlert(type: AlertType, message: string) {
    const newAlert: any = { type, message };
    this._alertSubject.next(newAlert);

    setTimeout(() => this.clearAlert(), 5000);
  }

  public clearAlert() {
    this._alertSubject.next(null);
  }
}
