import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "src/app/shared/services/authentication.service";

@Component({
  selector: 'user-menu',
  templateUrl: 'user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
})

export class UserMenuComponent implements OnInit {

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {

  }

  public logout() {
    this.authService.signOutExternal();
    this.router.navigate(["/"]);
  }
}
