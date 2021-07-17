import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, shareReplay } from 'rxjs/operators';
import { WeatherResponse } from 'src/app/models/common/weatherResponse';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  private apiUrl = 'api/weather/';

  constructor(private http: HttpClient) { }

  getCurrentWeather(): Observable<WeatherResponse> {
    const headers = new HttpHeaders
      ({
        'Content-Type': 'application/json'
      });

    return this.http.get<WeatherResponse>(this.apiUrl + "currentweather", { headers: headers })
      .pipe(
        tap((data: any) => {
          console.log(data);
        }),
        shareReplay(1),
        catchError(this.handleError)
      );
  }

  private handleError(err: any) {
    return throwError(err);
  }
}
