import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationModule } from './authentication/authentication.module';
import { GoogleSigninProvider } from './authentication/configs/google-login.config';
import { WrapperComponent } from './components/wrapper/wrapper.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSortModule } from '@angular/material/sort';
import { JwtInterceptor } from './shared/infrastructure/jwt-interceptor';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { environment } from 'src/environments/environment';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { RolesService } from './shared/services/roles.service';
import { UserMenuComponent } from './components/user-menu/user-menu.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ViewLocationsComponent } from './components/view-locations/view-locations.component';
import { LocationsRepositoryService } from './shared/services/locations-repository.service';
import { AddLocationComponent } from './components/add-location/add-location.component';
import { ViewEventsComponent } from './components/view-events/view-events.component';
import { EventsRepositoryService } from './shared/services/events-repository.service';
import { AddEventComponent } from './components/add-event/add-event.component';
import { ViewEventRequestsComponent } from './components/view-event-requests/view-event-requests.component';
import { EventReservationsRepositoryService } from './shared/services/event-reservations-repository.service';
import { ViewEventsJoinedComponent } from './components/view-events-joined/view-events-joined.component';
import { DialogWindowComponent } from './shared/components/dialog-window/dialog-window.component';
import { ViewParticipantsComponent } from './components/view-participants/view-participants.component';
import { MapLocationComponent } from './components/map-location/map-location.component'
import { PdfExportService } from './shared/services/pdf-export.service';
import { DatePipe } from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';

@NgModule({
  declarations: [
    AppComponent,
    WrapperComponent,
    HomePageComponent,
    UserMenuComponent,
    UserProfileComponent,
    ViewLocationsComponent,
    AddLocationComponent,
    ViewEventsComponent,
    AddEventComponent,
    ViewEventRequestsComponent,
    ViewEventsJoinedComponent,
    DialogWindowComponent,
    ViewParticipantsComponent,
    MapLocationComponent,
  ],
  imports: [
    MatDialogModule,
    BrowserModule,
    AppRoutingModule,
    MatButtonModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule,
    SocialLoginModule,
    AuthenticationModule,
    MatGridListModule,
    MatIconModule,
    MatCardModule,
    MatToolbarModule,
    MatTableModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatDividerModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,
    MatTooltipModule,
    MatTabsModule,
    MatSidenavModule,
    MatCheckboxModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: function tokenGetter() {
          return localStorage.getItem('token');
        },
        allowedDomains: [environment.urlAddress],
      },
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
    DatePipe,
    GoogleSigninProvider,
    JwtHelperService,
    RolesService,
    LocationsRepositoryService,
    EventsRepositoryService,
    EventReservationsRepositoryService,
    PdfExportService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
