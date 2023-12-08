import { SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ExternalAuthenticationDto } from 'src/app/authentication/interfaces/external-auth.dto';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Subscription } from 'rxjs';

@Component({
  selector: 'auth-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  private returnUrl: string = 'app';
  private subscription: Subscription;

  user: SocialUser;
  loggedIn: boolean;
  isPhonePortrait: boolean = false;
  nrCols: number = 2;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private responsive: BreakpointObserver
  ) { }

  ngOnInit(): void {
    this.subscription = this.authService.extAuthChanged.subscribe((user: SocialUser | null) => {
      this.loggedIn = user != null;

      if (user != null) {
        const externalAuth: ExternalAuthenticationDto = {
          provider: user.provider,
          idToken: user.idToken,
        };

        this.user = user;

        this.validateExternalAuthentication(externalAuth);
      }
    });

    this.responsive
      .observe([Breakpoints.HandsetPortrait, Breakpoints.TabletPortrait])
      .subscribe((result) => {
        this.isPhonePortrait = false;
        this.nrCols = 2;
        if (result.matches) {
          this.isPhonePortrait = true;
          this.nrCols = 1;
        }
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  private validateExternalAuthentication(
    externalAuth: ExternalAuthenticationDto
  ) {
    this.authService
      .externalLogin('User/ExternalLogin', externalAuth)
      .subscribe({
        next: (res) => {
          sessionStorage.setItem('token', res.token);
          sessionStorage.setItem('refreshToken', res.refreshToken);
          localStorage.setItem('roles', JSON.stringify(res.roles));
          this.router.navigate([this.returnUrl]);
        }
      });
  }
}
