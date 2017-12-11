import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyCreationComponent } from './company/company-creation/company-creation.component'
import { CompanyLoginComponent } from './company/company-login/company-login.component'


const routes: Routes = [
  {
    path: 'company/add',
    component: CompanyCreationComponent
  },
  {
    path: 'company/login',
    component: CompanyLoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
