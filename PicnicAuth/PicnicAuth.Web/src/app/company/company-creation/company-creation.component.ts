import { Component, OnInit } from '@angular/core';
import { CompaniesService } from '../companies.service'
import { AddCompanyArgument } from '../../models/add-company-argument'

@Component({
  selector: 'app-company-creation',
  templateUrl: './company-creation.component.html',
  styleUrls: ['./company-creation.component.css']
})
export class CompanyCreationComponent implements OnInit {
    addCompanyArgument: AddCompanyArgument = new AddCompanyArgument();

  constructor(private companiesService: CompaniesService) { }

  ngOnInit() {
  }

  createCompany() {
    this.companiesService.addCompany(this.addCompanyArgument).subscribe(id => console.log(id));
  }
}
