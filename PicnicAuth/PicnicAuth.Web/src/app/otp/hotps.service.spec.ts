import { TestBed, inject } from '@angular/core/testing';

import { HotpsService } from './hotps.service';

describe('HotpsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HotpsService]
    });
  });

  it('should be created', inject([HotpsService], (service: HotpsService) => {
    expect(service).toBeTruthy();
  }));
});
