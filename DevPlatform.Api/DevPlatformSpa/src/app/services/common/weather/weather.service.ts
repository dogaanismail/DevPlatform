import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, shareReplay } from 'rxjs/operators';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http: HttpClient) { }

  private API_key: string = "873510ede29b47c8f9eed3e8212d8d9e";
  private apiUrl = 'https://api.openweathermap.org/data/2.5';


  getCurrentWeather(city: string) {
    return this.http.get<object>(`${this.apiUrl}/weather?q=${city}&appid=${this.API_key}`);
  }

  getForecast(city: string) {
    return this.http.get<object>(`${this.apiUrl}/forecast?q=${city}&appid=${this.API_key}`);
  }

  getUv(lat: number, lon: number) {
    let startDate = Math.round(+moment(new Date()).subtract(1, 'week').toDate() / 1000);
    let endDate = Math.round(+moment(new Date()).add(1, 'week').toDate() / 1000);
    return this.http.get(`${this.apiUrl}/uvi/history?lat=${lat}&lon=${lon}&start=${startDate}&end=${endDate}&appid=${this.API_key}`)
  }
}
