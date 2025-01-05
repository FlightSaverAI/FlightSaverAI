import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environments';
import { LoginFormData, RegistrationFormData } from '@flight-saver/authentication/models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _url = environment.url;
  private _httpClient = inject(HttpClient);

  authentication(formData: LoginFormData) {
    return this._httpClient.post<{ token: string }>(`${this._url}/Auth/Login`, formData);
  }

  registration(formData: RegistrationFormData) {
    return this._httpClient.post<{ token: string }>(`${this._url}/Auth/Register`, formData);
  }
}
