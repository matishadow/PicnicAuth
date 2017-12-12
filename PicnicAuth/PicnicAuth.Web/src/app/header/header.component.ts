import { Component } from '@angular/core';
import { CompanyService } from '../company/company.service';

@Component({
  selector: 'fg-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass']
})
export class HeaderComponent {
  menuOpened: boolean;

  constructor(private companyService: CompanyService) {
    this.menuOpened = false;
  }

  changeMenuState() {
    this.menuOpened = !this.menuOpened;
  }

  get isLoggedIn() {
    return this.companyService.isLoggedIn();
  }
}
