import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, shareReplay } from 'rxjs/operators';
import { GeoLookupModel } from '../../../models/common/geolookup';

@Injectable({
  providedIn: 'root'
})
export class GeoLookupService {

  private apiUrl = 'api/geolookup/';

  constructor(
    private http: HttpClient
  ) { }

  getCityAndCountryInformations(): Observable<GeoLookupModel> {
    const headers = new HttpHeaders
      ({
        'Content-Type': 'application/json'
      });

    return this.http.get<GeoLookupModel>(this.apiUrl + "cityandcountryinformations", { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        shareReplay(1),
        catchError(this.handleError)
      );
  }

  getCurrentCountryName(): Observable<string> {
    const headers = new HttpHeaders
      ({
        'Content-Type': 'application/json'
      });

    return this.http.get<string>(this.apiUrl + "currentcountryname", { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        shareReplay(1),
        catchError(this.handleError)
      );
  }

  getCurrentCountryIsoCode(): Observable<string> {
    const headers = new HttpHeaders
      ({
        'Content-Type': 'application/json'
      });

    return this.http.get<string>(this.apiUrl + "currentcountryisocode", { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        shareReplay(1),
        catchError(this.handleError)
      );
  }


  private handleError(err: any) {
    return throwError(err);
  }
}
