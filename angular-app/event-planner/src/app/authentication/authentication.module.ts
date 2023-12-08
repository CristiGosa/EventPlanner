import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { LoginComponent } from './login/login.component';

import { SocialLoginModule, SocialAuthServiceConfig, GoogleSigninButtonDirective, GoogleSigninButtonModule, SocialAuthService } from '@abacritt/angularx-social-login';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import {MatToolbarModule} from '@angular/material/toolbar';
@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    GoogleSigninButtonModule,
    MatGridListModule,
    MatCardModule,
    MatToolbarModule
  ]
})
export class AuthenticationModule { }
