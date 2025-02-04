import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';

@Injectable()
export class PostService {
  private _http = inject(HttpClient);

  public getUserPosts() {
    return this._http.get<any>(`${environment.url}/post/user`);
  }

  public addPost(form: any) {
    const { city, country, image, content } = form;

    const formData = new FormData();

    if (image) {
      const blob = this._dataURLtoBlob(image);
      const file = new File([blob], 'uploaded-image.png', { type: blob.type });
      formData.append('image', file, 'image.png');
    }

    const params = {
      location: `${country}, ${city.trim()}`,
      content,
    };

    return this._http.post<any>(`${environment.url}/post`, formData, {
      params,
    });
  }

  public editPost(form: any, postId: string) {
    const { city, country, content, image } = form;

    const formData = new FormData();

    if (image) {
      const blob = this._dataURLtoBlob(image);
      const file = new File([blob], 'uploaded-image.png', { type: blob.type });
      formData.append('image', file, 'image.png');
    }

    const params: any = {
      id: postId,
      location: `${country}, ${city.trim()}`,
      content,
    };

    return this._http.put<any>(`${environment.url}/post`, formData, {
      params,
    });
  }

  public deletePost(postId: string) {
    return this._http.delete<any>(`${environment.url}/post/${postId}`);
  }

  private _dataURLtoBlob(dataURL: string): Blob {
    const arr = dataURL.split(',');
    const mimeMatch = arr[0].match(/:(.*?);/);
    const mime = mimeMatch ? mimeMatch[1] : 'image/png';
    const byteString = atob(arr[1]);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const uint8Array = new Uint8Array(arrayBuffer);

    for (let i = 0; i < byteString.length; i++) {
      uint8Array[i] = byteString.charCodeAt(i);
    }

    return new Blob([arrayBuffer], { type: mime });
  }
}
