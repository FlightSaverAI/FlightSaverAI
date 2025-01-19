import { inject, Injectable } from '@angular/core';
import { FlightFormService } from './flight-form.service';
import { Router } from '@angular/router';

@Injectable()
export class FlightCreationFacadeService {
  private _flightForm = inject(FlightFormService);
  private _router = inject(Router);
  private _homeUrl = '/authorized/home';

  public saveFlight(payload: any) {
    this._flightForm.addFlight(payload).subscribe({
      next: () => this._router.navigateByUrl(this._homeUrl),
      error: (error) => alert(error),
    });
  }
}
