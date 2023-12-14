import { inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { CanActivateFn, Router } from '@angular/router';
import { RolesService } from '../services/roles.service';

export function userGuard(): CanActivateFn {
  return () => {
    const oauthService: AuthenticationService = inject(AuthenticationService);
    const route: Router = inject(Router);
    const rolesService: RolesService = inject(RolesService);

    const isUser: boolean = rolesService.isUser();

    if (oauthService.hasAccess()) {
      if (!oauthService.isTokenExpired()) {
        if (isUser) {
          return true;
        } else {
            route.navigate(['/']);
            return false;
        }
      }else{
        oauthService.signOutExternal();
        route.navigate(['/']);
        return false;
      }
    }

    route.navigate(['/']);
    return false;
  };
}