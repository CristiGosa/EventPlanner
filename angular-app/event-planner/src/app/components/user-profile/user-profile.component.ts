import { SocialUser } from "@abacritt/angularx-social-login";
import { Component } from "@angular/core";
import { AuthenticationService } from "src/app/shared/services/authentication.service";

@Component({
  selector: 'user-profile',
  templateUrl: 'user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})

export class UserProfileComponent {

  username: string = 'Username';
  showCard: boolean = false;
  user: SocialUser;

  constructor(private authService: AuthenticationService) {
    this.authService.extAuthChanged.subscribe((user: SocialUser | null) => {
      this.username = 'Username';
      if (user != null) {
        this.username = user.name;
        this.user = user;
      }
    });
  }

  toggleCard() {
    this.showCard = !this.showCard
  }
  isMenuOpened: boolean = false;

  toggleMenu(): void {
    this.isMenuOpened = !this.isMenuOpened;
  }

  clickedOutside(): void {
    this.isMenuOpened = false;
  }
}