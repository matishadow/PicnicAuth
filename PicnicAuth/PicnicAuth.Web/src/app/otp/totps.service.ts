import { Injectable } from "@angular/core";
import { OneTimePassword } from "../models/one-time-password";
import { OtpValidationResult } from "../models/otp-validation-result";
import { Observable } from "rxjs/Observable";
import { ApiService } from "../api/api.service"

@Injectable()
export class TotpsService {

  constructor(private readonly api: ApiService) { }

  getTotpForAuthUser(userId: string): Observable<OneTimePassword> {
    return this.api.get(`AuthUsers/${userId}/totp`);
  }

  validateTotpForAuthUser(userId: string, totp: string): Observable<OtpValidationResult> {
    return this.api.get(`AuthUsers/${userId}/totp/${totp}`);
  }
}
