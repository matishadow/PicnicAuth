import { Injectable } from "@angular/core";
import { Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ApiService } from "../api/api.service";
import { TokenResponse } from "../models/token-response"
import { endpoints } from "../consts/endpoints";


@Injectable()
export class TokensApiService {

  constructor(private readonly api: ApiService) { }

  login(username: string, password: string): Observable<TokenResponse> {
    return this.api.post(
      endpoints.login,
      `username=${username}&password=${password}&grant_type=password`,
      new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' })
    );
  }
}
