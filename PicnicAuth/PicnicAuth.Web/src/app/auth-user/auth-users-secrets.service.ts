import { Injectable } from "@angular/core";
import { Headers } from "@angular/http";
import { CreatedAuthUser } from "../models/created-auth-user";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";
import { ApiService } from "../api/api.service";

@Injectable()
export class AuthUsersSecretsService {

  constructor(private readonly api: ApiService) { }


  generateNewSecret(authUserId: string): Observable<CreatedAuthUser> {
      return this.api.patch(`AuthUsers/${authUserId}/secret`);
  }

}
