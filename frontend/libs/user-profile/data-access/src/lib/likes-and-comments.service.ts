import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environments';

@Injectable()
export class LikesAndCommentsService {
  private _http = inject(HttpClient);

  public likePost(postId: string) {
    return this._http.post(`${environment.url}/post/${postId}/like`, null);
  }

  public unlikePost(postId: string) {
    return this._http.post(`${environment.url}/post/${postId}/unlike`, null);
  }

  public getPostComments(postId: string) {
    return this._http.get<any>(`${environment.url}/comment`, {
      params: { postId },
    });
  }

  public addComment(postId: string, content: string) {
    return this._http.post<any>(`${environment.url}/comment`, null, {
      params: { postId, content },
    });
  }

  public updateComment(commentId: string, content: string) {
    return this._http.put<any>(`${environment.url}/comment/`, null, {
      params: { id: commentId, content },
    });
  }

  public deleteComment(commentId: string) {
    return this._http.delete<any>(`${environment.url}/comment/${commentId}`);
  }
}
