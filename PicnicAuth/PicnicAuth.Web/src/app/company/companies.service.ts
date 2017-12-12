import { Injectable } from "@angular/core";
import { LoggedCompany } from "../models/logged-company";
import { IdentityResult } from "../models/identity-result";
import { AddCompanyArgument } from "../models/add-company-argument";
import { Observable } from "rxjs/Observable";
import { ApiService } from "../api/api.service";

@Injectable()
export class CompaniesService {
  constructor(private readonly api: ApiService) { }

  getLoggedCompany(): Observable<LoggedCompany> {
    return this.api.get("Companies/Me");
  }

  addCompany(addCompanyArgument: AddCompanyArgument): Observable<IdentityResult> {
    return this.api.post("Companies", addCompanyArgument);
  }
}
