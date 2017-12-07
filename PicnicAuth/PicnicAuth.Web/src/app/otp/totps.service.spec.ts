import { TestBed, inject } from '@angular/core/testing';

import { TotpsService } from './totps.service';

describe('TotpsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TotpsService]
    });
  });

  it('should be created', inject([TotpsService], (service: TotpsService) => {
    expect(service).toBeTruthy();
  }));
});
