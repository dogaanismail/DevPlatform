/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { StoryService } from './story.service';

describe('Service: Story', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StoryService]
    });
  });

  it('should ...', inject([StoryService], (service: StoryService) => {
    expect(service).toBeTruthy();
  }));
});
