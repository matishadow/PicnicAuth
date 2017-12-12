import { TestBed, inject } from '@angular/core/testing';

import { AuthUsersService } from './auth-users.service';

describe('AuthUsersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthUsersService]
    });
  });

  it('should be created', inject([AuthUsersService], (service: AuthUsersService) => {
    expect(service).toBeTruthy();
  }));
});
