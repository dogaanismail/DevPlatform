import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { AuthService } from "../user/auth/auth.service";
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, shareReplay } from 'rxjs/operators';
import { Story } from '../../models/story/story';

@Injectable({
  providedIn: 'root'
})
export class StoryService {

  private storyUrl = 'api/stories/';

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  createStory(story: any): Observable<Story> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken
      });
    return this.http.post(this.storyUrl + "createstory", story, { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        catchError(this.handleError)
      );
  }

  getStories(): Observable<Story[]> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken,
        'Content-Type': 'application/json'
      });

    return this.http.get<Story[]>(this.storyUrl + "storylist", { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        shareReplay(1),
        catchError(this.handleError)
      );
  }

  private handleError(err: any) {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }

}
