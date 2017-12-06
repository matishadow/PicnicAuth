import { Injectable } from "@angular/core";
import { Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ApiService } from "../api/api.service";
import { TokenResponse } from "../models/token-response"


@Injectable()
export class TokensApiService {

  constructor(private readonly api: ApiService) { }

  login(username: string, password: string): Observable<TokenResponse> {
    return this.api.post(
      "tokens",
      `username=${username}&password=${password}&grant_type=password`,
      new Headers({ "Content-Type": "application/x-www-form-urlencoded" })
    );
  }
}
