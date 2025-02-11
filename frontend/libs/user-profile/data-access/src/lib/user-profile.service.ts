import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';

@Injectable()
export class UserProfileService {
  private _http = inject(HttpClient);

  public getUserProfileData(userId?: any | null) {
    console.log(userId);

    return this._http.get<any>(`${environment.url}/user/info`, {
      params: userId ? { userId } : {},
    });
  }

  public updatePassword(form: any) {
    return this._http.put<any>(
      `${environment.url}/user`,
      {},
      {
        params: { ...form },
      }
    );
  }

  public updateUserPhoto(photo: Blob, photoType: 'Background' | 'Profile') {
    const formBlob = new FormData();
    const type = photoType === 'Profile' ? 'ProfilePictureImage' : 'BackgroundPictureImage';

    formBlob.append(type, photo, 'image.png');

    return this._http.put<any>(`${environment.url}/user`, formBlob);
  }
}
