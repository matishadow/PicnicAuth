import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { CompanyService } from '../company/company.service';
import { Router } from '@angular/router';

@Injectable()
export class NotLoggedInGuardService implements CanActivate {

  constructor(
    private companyService: CompanyService,
    private router: Router
  ) { }

  canActivate(): boolean {
    const loggedin = this.companyService.isLoggedIn();
    if (loggedin)
      this.router.navigate(["/auth-users"]);
    return loggedin;
  }
}
