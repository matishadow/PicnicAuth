import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyCreationComponent } from './company/company-creation/company-creation.component'
import { CompanyLoginComponent } from './company/company-login/company-login.component'
import { HomeComponent } from './base/home/home.component'


const routes: Routes = [
  {
    path: 'company/add',
    component: CompanyCreationComponent
  },
  {
    path: 'company/login',
    component: CompanyLoginComponent
  },
  {
    path: '',
    component: HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
