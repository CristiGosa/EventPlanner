import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { LoginComponent } from './login/login.component';

import { GoogleSigninButtonModule } from '@abacritt/angularx-social-login';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ErrorMessageComponent } from '../shared/components/error-message/error-message.component';
@NgModule({
  declarations: [
    LoginComponent,
    ErrorMessageComponent
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    GoogleSigninButtonModule,
    MatGridListModule,
    MatCardModule,
    MatToolbarModule
  ],
  exports: [
    ErrorMessageComponent
  ]
})
export class AuthenticationModule { }
