import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  private _isLoadingSubject = new BehaviorSubject<boolean>(false);

  public get isLoading$(): Observable<boolean> {
    return this._isLoadingSubject.asObservable();
  }

  public show() {
    this._isLoadingSubject.next(true);
  }

  public hide() {
    this._isLoadingSubject.next(false);
  }
}
