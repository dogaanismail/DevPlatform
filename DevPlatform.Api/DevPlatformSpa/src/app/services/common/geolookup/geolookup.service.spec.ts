/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GeolookupService } from './geolookup.service';

describe('Service: Geolookup', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GeolookupService]
    });
  });

  it('should ...', inject([GeolookupService], (service: GeolookupService) => {
    expect(service).toBeTruthy();
  }));
});
