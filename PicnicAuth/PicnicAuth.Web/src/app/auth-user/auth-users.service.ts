import { Injectable } from "@angular/core";
import { Headers } from "@angular/http";
import { CreatedAuthUser } from "../models/created-auth-user";
import { AuthUsersInCompany } from "../models/auth-users-in-company";
import { AddAuthUserArgument } from "../models/add-auth-user-argument";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";
import { ApiService } from "../api/api.service";

@Injectable()
export class AuthUsersService {

  constructor(private readonly api: ApiService) { }

  addAuthUser(addAuthUserArgument: AddAuthUserArgument): Observable<CreatedAuthUser> {
    return this.api.post("AuthUsers", addAuthUserArgument);
  }

  getAuthUsersForLoggedCompany(page: number = 1, pageCount: number = 10): Observable<AuthUsersInCompany> {
    return this.api.get(`Companies/Me/AuthUsers?page=${page}&pageCount=${pageCount}`);
  }
}
