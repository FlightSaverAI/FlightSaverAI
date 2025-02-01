import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';

@Injectable()
export class SettingsService {
  private _http = inject(HttpClient);

  public getUserProfileData() {
    return this._http.get<any>(`${environment.url}/user/info`);
  }

  public updateUserProfileData(form: any, photoType?: any) {
    //TOFIX
    if (form instanceof Blob) {
      const formBlob = new FormData();
      const type = photoType === 'Profile' ? 'ProfilePictureImage' : 'BackgroundPictureImage';

      formBlob.append(type, form, 'image.png');
      return this._http.put<any>(`${environment.url}/user`, formBlob);
    }

    return this._http.put<any>(
      `${environment.url}/user`,
      {},
      {
        params: { ...form },
      }
    );
  }
}
