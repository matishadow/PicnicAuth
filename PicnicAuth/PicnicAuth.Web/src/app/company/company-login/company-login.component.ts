import { Component, OnInit } from '@angular/core';
import { NotifierService } from "../../base/notifier.service";
import { TokensApiService } from "../../tokens/tokens-api.service";

@Component({
  selector: 'app-company-login',
  templateUrl: './company-login.component.html',
  styleUrls: ['./company-login.component.sass']
})
export class CompanyLoginComponent implements OnInit {
  username: string;
  password: string;
  apiKey: string;

  constructor(private notifierService: NotifierService,
    private tokensApiService: TokensApiService) { }

  ngOnInit() {
  }

  login() {
    this.notifierService.clearAll();

    this.tokensApiService
      .login(this.username, this.password)
      .subscribe(tokenResponse => {
        this.notifierService.success("API key successfully retrieved");
        this.apiKey = tokenResponse.access_token;
      }, (error) => {
        this.notifierService.error(error);
      });
  }

}
