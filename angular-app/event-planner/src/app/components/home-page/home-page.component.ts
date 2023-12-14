import { Component } from "@angular/core";
import { RolesService } from "src/app/shared/services/roles.service";

@Component({
  selector: 'home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})

export class HomePageComponent {

  constructor(public rolesService: RolesService) { }


  isAdmin(): boolean {
    return this.rolesService.isAdmin();
  }
  isStockManager(): boolean {
    return this.rolesService.isStockManager();
  }
}
