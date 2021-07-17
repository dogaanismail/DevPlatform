import { Component, Input, OnInit } from '@angular/core';

/* Services */
import { GeoLookupService } from '../../../../services/common/geolookup/geolookup.service';
import { WeatherService } from '../../../../services/common/weather/Weather.service';

/* Models */
import { GeoLookupModel } from '../../../../models/common/geolookup';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';
import { WeatherResponse } from 'src/app/models/common/weatherResponse';

@Component({
  selector: 'app-timeline-weather',
  templateUrl: './timeline-weather.component.html',
  styleUrls: ['./timeline-weather.component.scss']
})
export class TimelineWeatherComponent implements OnInit {

  constructor(
  ) { }

  @Input() currentWeather: WeatherResponse;

  ngOnInit() {

  }

}
