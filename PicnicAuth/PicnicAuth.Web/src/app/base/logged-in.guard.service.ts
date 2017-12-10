import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { CompanyService } from '../company/company.service';

@Injectable()
export class LoggedInGuardService implements CanActivate {

  constructor(private readonly companyService: CompanyService) { }

  canActivate(): boolean {
    return this.companyService.isLoggedIn();
  }
}
