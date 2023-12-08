import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { BehaviorSubject } from 'rxjs';

import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { ExternalAuthenticationDto } from 'src/app/authentication/interfaces/external-auth.dto';
import { AuthenticationResponseDto } from 'src/app/authentication/interfaces/authentication-response.dto';
import { RefreshTokenResponseDto } from '../interfaces/refreshToken/refresh-token-response-dto';
import { RefreshTokenRequestDto } from '../interfaces/refreshToken/refresh-token-request-dto';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private extAuthChangeSub = new BehaviorSubject<SocialUser | null>(null);
  public extAuthChanged = this.extAuthChangeSub.asObservable();
  user: SocialUser | null;

  constructor(
    private http: HttpClient, 
    private envUrl: EnvironmentUrlService,
    private externalAuthService: SocialAuthService,
    private jwtHelperService: JwtHelperService
  ){
     this.getUser();
     this.setUser();
  }

  changeAuth(user:SocialUser | null){
    this.extAuthChangeSub.next(user);
  }

  private getUser(): void{
    this.externalAuthService.authState.subscribe((user: SocialUser) => {
      this.user = user;
      if(user != null){
        localStorage.setItem('socialUser', JSON.stringify(user));
        this.extAuthChangeSub.next(user);
      }
      
    });
  }

  private setUser(): void{
    var user = localStorage.getItem('socialUser');
    let socialUser : SocialUser;

    if(user !== null){
      socialUser = (JSON.parse(user));
      this.extAuthChangeSub.next(socialUser);
    }
  }

  public externalLogin(route: string, body: ExternalAuthenticationDto) {
    return this.http.post<AuthenticationResponseDto>(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body
    );
  }

  public refreshToken(route: string, body: RefreshTokenRequestDto) {
    return this.http.post<RefreshTokenResponseDto>(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body
    );
  }

  public signOutExternal() {
    sessionStorage.clear();
    localStorage.clear();
    if (this.user != null) {
      this.externalAuthService.signOut();
    }
    this.sendExtAuthStateChangeNotification(null);
  }

  private sendExtAuthStateChangeNotification(
    isAuthenticated: SocialUser | null
  ) {

    this.extAuthChangeSub.next(isAuthenticated);
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return envAddress + '/' + route;
  }
  
  hasAccess(): boolean {
    if (sessionStorage.getItem('token')) {
      return true;
    }
    return false;
  }

  isTokenExpired(): boolean {
    var token = sessionStorage.getItem('token');
    if (token == null) {
      return true;
    }

    let isExpired: boolean = this.jwtHelperService.isTokenExpired(token);

    return isExpired;
  }
}
