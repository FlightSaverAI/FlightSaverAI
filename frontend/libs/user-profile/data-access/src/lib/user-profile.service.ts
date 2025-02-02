import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';

@Injectable()
export class UserProfileService {
  private _http = inject(HttpClient);

  public getUserProfileData() {
    return this._http.get<any>(`${environment.url}/user/info`);
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

  public getUserPosts() {
    return this._http.get<any>(`${environment.url}/post/user`);
  }

  public addPost(form: any) {
    const { City, Country, Content } = form;

    return this._http.post<any>(`${environment.url}/post`, form.Image, {
      params: {
        'Post.Location': `${Country}, ${City}`,
        'Post.Content': Content,
      },
    });
  }
}
