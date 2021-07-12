import { Component, OnInit } from '@angular/core';

/* Services */
import { GeoLookupService } from '../../../../services/common/geolookup/geolookup.service';
import { WeatherService } from '../../../../services/common/weather/Weather.service';

/* Models */
import { GeoLookupModel } from '../../../../models/common/geolookup';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';

@Component({
  selector: 'app-timeline-weather',
  templateUrl: './timeline-weather.component.html',
  styleUrls: ['./timeline-weather.component.scss']
})
export class TimelineWeatherComponent implements OnInit {

  geoLookupModel: GeoLookupModel;
  currentWeather: any = <any>{};
  currentDate: Date;
  msg: string;
  constructor(
    private geoLookupService: GeoLookupService,
    private weatherService: WeatherService
  ) { }

  ngOnInit() {
    this.getCurrentLocation();
    this.currentDate = new Date();
  }

  private getCurrentLocation(): void {
    this.geoLookupService.getCityAndCountryInformations().subscribe((response: any) => {
      this.geoLookupModel = response.result as GeoLookupModel;
    }, err => { }, () => {
      this.getCurrentWeather(this.geoLookupModel.currentCityName);
    })
  }


  private getCurrentWeather(currentCityName: string) {
    this.weatherService.getCurrentWeather(currentCityName)
      .subscribe(response => {
        console.log(response);
        this.currentWeather = response;
      }, err => {
        if (err.error && err.error.message) {
          alert(err.error.message);
          this.msg = err.error.message;
          return;
        }
        alert('Failed to get weather.');
      }, () => {
      })
  }
}
