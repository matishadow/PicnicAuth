import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyCreationComponent } from './company/company-creation/company-creation.component'
import { CompanyLoginComponent } from './company/company-login/company-login.component'
import { HomeComponent } from './base/home/home.component'
import { HowToComponent } from './how-to/how-to.component'
import { InfoComponent } from './info/info.component'


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
  },
  {
    path: 'how-to',
    component: HowToComponent
  },
  {
    path: 'info',
    component: InfoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
