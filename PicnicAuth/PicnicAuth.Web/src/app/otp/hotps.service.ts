import { Injectable } from "@angular/core";
import { OneTimePassword } from "../models/one-time-password";
import { OtpValidationResult } from "../models/otp-validation-result";
import { Observable } from "rxjs/Observable";
import { ApiService } from "../api/api.service"

@Injectable()
export class HotpsService {

    constructor(private readonly api: ApiService) { }

    getHotpForAuthUser(userId: string): Observable<OneTimePassword> {
        return this.api.get(`AuthUsers/${userId}/hotp`);
    }

    validateHotpForAuthUser(userId: string, hotp: string): Observable<OtpValidationResult> {
        return this.api.get(`AuthUsers/${userId}/hotp/${hotp}`);
    }
}
