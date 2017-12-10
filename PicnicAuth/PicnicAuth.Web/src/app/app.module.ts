import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CompaniesService } from "./company/companies.service";
import { ApiService } from "./api/api.service";
import { CompanyService } from "./company/company.service";
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NotifierService } from './base/notifier.service'

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { CompanyCreationComponent } from './company/company-creation/company-creation.component';
import { HeaderComponent } from './header/header.component';
import { NotifierComponent } from './notifier/notifier.component';
import { TokensComponent } from './tokens/tokens/tokens.component';


@NgModule({
  declarations: [
    AppComponent,
    CompanyCreationComponent,
    HeaderComponent,
    NotifierComponent,
    TokensComponent,
  ],
  imports: [
    HttpModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [CompaniesService, ApiService, CompanyService, NotifierService],
  bootstrap: [AppComponent]
})
export class AppModule { }
