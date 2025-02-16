import { inject, Injectable } from '@angular/core';
import { FlightFormService } from './flight-form.service';
import { Router } from '@angular/router';
import { AlertService } from '@shared/data-access';

@Injectable()
export class FlightCreationFacadeService {
  private _flightForm = inject(FlightFormService);
  private _alertService = inject(AlertService);
  private _router = inject(Router);
  private _homeUrl = '/authorized/home';

  public saveFlight(payload: any) {
    this._flightForm.addFlight(payload).subscribe({
      next: () => {
        this._router.navigateByUrl(this._homeUrl);
        this._alertService.showAlert('success', 'Flight has been added successfuly');
      },
      error: (error) => alert(error),
    });
  }
}
