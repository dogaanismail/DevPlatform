/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { AlertifyService } from './alertify.service';

describe('Service: Alertify', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlertifyService]
    });
  });

  it('should ...', inject([AlertifyService], (service: AlertifyService) => {
    expect(service).toBeTruthy();
  }));
});
