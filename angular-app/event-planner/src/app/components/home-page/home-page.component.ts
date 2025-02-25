import { SocialUser } from "@abacritt/angularx-social-login";
import { Component } from "@angular/core";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { RolesService } from "src/app/shared/services/roles.service";

@Component({
  selector: 'home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})

export class HomePageComponent {
  username: string;

  constructor(public rolesService: RolesService, public authService: AuthenticationService) {
    this.authService.extAuthChanged.subscribe((user: SocialUser | null) => {
      this.username = 'Username';
      if (user != null) {
        this.username = user.name;
      }
    });
  }


  isAdmin(): boolean {
    return this.rolesService.isAdmin();
  }
  isOrganizer(): boolean {
    return this.rolesService.isOrganizer();
  }
  getWelcomeText(): string {
    return "Bine ai revenit, " + this.username;
  }
}
