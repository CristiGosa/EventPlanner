import { SocialUser } from "@abacritt/angularx-social-login";
import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "src/app/shared/services/authentication.service";

@Component({
  selector: 'user-profile',
  templateUrl: 'user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})

export class UserProfileComponent implements OnInit {

  username: string | undefined = 'Username';
  photoUrl: string | undefined;
  showCard: boolean = false;
  user: SocialUser | null = null;

  constructor(private authService: AuthenticationService) {}

  ngOnInit(): void {
    this.authService.extAuthChanged.subscribe((user: SocialUser | null) => {
      this.user = user;
    });
    if (this.user != null) {
      setTimeout(() => {
        this.username = this.user?.name;
        this.photoUrl = this.user?.photoUrl;
      }, 50);
    }
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