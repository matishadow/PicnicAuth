import { Injectable } from "@angular/core";
import { Headers } from "@angular/http";
import { TokenResponse } from "../models/token-response";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";

@Injectable()
export class CompanyService {
  authHeaders = new Headers();
  private company: string;

  constructor(private readonly router: Router) {
    const token = localStorage.getItem("token");
    if (token)
      this.setAuthToken(token);
  }

  onLoggedIn(tokenResponse: TokenResponse) {
    this.setAuthToken(tokenResponse.access_token);
    this.company = tokenResponse.userName;
  }

  setAuthToken(token: string) {
    this.authHeaders.set("Authorization", "Bearer " + token);
    localStorage.setItem("token", token);
  }

  set companyUserName(userName: string) {
    if (!userName)
      localStorage.removeItem("userName");
    else
      localStorage.setItem("userName", userName);
    this.company = userName;
  }

  get companyUserName(): string {
    if (!this.company)
      this.company = localStorage.getItem("userName");
    return this.company;
  }

  logout() {
    this.authHeaders = new Headers();
    localStorage.removeItem("token");
    this.company = null;
  }

  isLoggedIn(): boolean {
    return this.company != null;
  }

  unauthorized() {
    this.logout();
    this.router.navigate(["/company/login"]);
  }
}
