import { TestBed, inject } from '@angular/core/testing';

import { TokensApiService } from './tokens-api.service';

describe('TokensService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TokensApiService]
    });
  });

  it('should be created', inject([TokensApiService], (service: TokensApiService) => {
    expect(service).toBeTruthy();
  }));
});
