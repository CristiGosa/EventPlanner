import { inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { CanActivateFn, Router } from '@angular/router';
import { RolesService } from '../services/roles.service';
import { RefreshTokenRequestDto } from '../interfaces/refreshToken/refresh-token-request-dto';

export function appGuard(): CanActivateFn {
  return () => {
    const authService: AuthenticationService = inject(AuthenticationService);
    const rolesService: RolesService = inject(RolesService);
    const route: Router = inject(Router);

    const isAdmin: boolean = rolesService.isAdmin();
    const isUser: boolean = rolesService.isUser();
    const isStockManager: boolean = rolesService.isOrganizer();

    if (authService.hasAccess()) {
      if (!authService.isTokenExpired()) {
        if (isAdmin || isStockManager || isUser) {
          return true;
        }
      } else {
        var user = localStorage.getItem('socialUser');
        let socialUser;
        if (user != null) {
          socialUser = JSON.parse(user);
        }
        let refreshToken = sessionStorage.getItem('refreshToken');
        if (refreshToken != null) {
          let request: RefreshTokenRequestDto = {
            email: socialUser.email,
            oldRefreshToken: refreshToken,
          };
          authService.refreshToken('User/RefreshToken', request).subscribe({
            next: (res) => {
              sessionStorage.setItem('token', res.token);
              sessionStorage.setItem('refreshToken', res.refreshToken);
            },
          });
          return true;
        } else {
          route.navigate(['/']);
          return false;
        }
      }
    }

    route.navigate(['/']);
    return false;
  };
}
