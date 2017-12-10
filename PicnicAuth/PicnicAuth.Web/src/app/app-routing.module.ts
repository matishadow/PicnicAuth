import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyCreationComponent } from './company/company-creation/company-creation.component'

const routes: Routes = [
  {
    path: 'company/add',
    component: CompanyCreationComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
