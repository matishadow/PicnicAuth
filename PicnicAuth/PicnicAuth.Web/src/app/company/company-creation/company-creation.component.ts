import { Component, OnInit } from '@angular/core';
import { CompaniesService } from '../companies.service'
import { AddCompanyArgument } from '../../models/add-company-argument'
import { NotifierService } from "../../base/notifier.service";

@Component({
  selector: 'app-company-creation',
  templateUrl: './company-creation.component.html',
  styleUrls: ['./company-creation.component.css']
})
export class CompanyCreationComponent implements OnInit {
    addCompanyArgument: AddCompanyArgument = new AddCompanyArgument();

    constructor(private companiesService: CompaniesService,
      private notifierService: NotifierService) { }

  ngOnInit() {
  }

  createCompany() {
    this.notifierService.clearAll();

    this.companiesService
      .addCompany(this.addCompanyArgument)
      .subscribe(() => {
        this.notifierService.success("New company account has been created!");
      }, (error) => {
        this.notifierService.error(error);
      });
  }
}
