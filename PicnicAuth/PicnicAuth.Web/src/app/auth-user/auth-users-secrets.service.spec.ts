import { TestBed, inject } from '@angular/core/testing';

import { AuthUsersSecretsService } from './auth-users-secrets.service';

describe('AuthUsersSecretsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthUsersSecretsService]
    });
  });

  it('should be created', inject([AuthUsersSecretsService], (service: AuthUsersSecretsService) => {
    expect(service).toBeTruthy();
  }));
});
