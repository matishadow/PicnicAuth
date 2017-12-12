import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CompaniesService } from "./company/companies.service";
import { ApiService } from "./api/api.service";
import { CompanyService } from "./company/company.service";
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NotifierService } from './base/notifier.service'
import { TokensApiService } from './tokens/tokens-api.service'

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './base/home/home.component';
import { CompanyCreationComponent } from './company/company-creation/company-creation.component';
import { HeaderComponent } from './header/header.component';
import { NotifierComponent } from './notifier/notifier.component';
import { TokensComponent } from './tokens/tokens/tokens.component';
import { CompanyLoginComponent } from './company/company-login/company-login.component';


@NgModule({
  declarations: [
    AppComponent,
    CompanyCreationComponent,
    HeaderComponent,
    NotifierComponent,
    TokensComponent,
    CompanyLoginComponent,
    HomeComponent,
  ],
  imports: [
    HttpModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [CompaniesService, ApiService, CompanyService, NotifierService, TokensApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
