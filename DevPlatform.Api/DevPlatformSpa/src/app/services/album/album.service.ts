import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AlbumCreate } from 'src/app/models/album/albumCreate';
import { AuthService } from '../user/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {

  constructor(private httpClient: HttpClient,
    private authService: AuthService) { }

  albumUrl = "api/album/";

  createAlbum(post: AlbumCreate): Observable<AlbumCreate> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken,
        "Access-Control-Allow-Origin":'*'
      });
    return this.httpClient.post(this.albumUrl + "createalbum", post, { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
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
