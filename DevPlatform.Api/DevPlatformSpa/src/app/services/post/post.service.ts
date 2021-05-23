
import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { AuthService } from "../user/auth/auth.service";
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, shareReplay } from 'rxjs/operators';
import { Post } from '../../models/post/post';

@Injectable({
  providedIn: "root"
})
export class PostService {
  private postUrl = 'api/posts/';

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) { }


  createPost(post: any): Observable<Post> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken
      });
    return this.http.post(this.postUrl + "createpost", post, { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        catchError(this.handleError)
      );
  }


  createGif(post: any): Observable<Post> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken
      });
    return this.http.post(this.postUrl + "creategif", post, { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
      );
  }

  getPosts(): Observable<Post[]> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken,
        'Content-Type': 'application/json'
      });

    return this.http.get<Post[]>(this.postUrl + "postlist", { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        shareReplay(1),
        catchError(this.handleError)
      );
  }

  createComment(comment: any): Observable<Post> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken,
        'Content-Type': 'application/json'
      });
    return this.http.post(this.postUrl + "createcomment", comment, { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        catchError(this.handleError)
      );
  }

  private handleError(err: any) {
    return throwError(err);
  }

}

