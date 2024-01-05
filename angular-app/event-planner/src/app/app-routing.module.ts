import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { appGuard } from './shared/guards/app.guard';
import { ViewLocationsComponent } from './components/view-locations/view-locations.component';
import { adminGuard } from './shared/guards/admin.guard';
import { AddLocationComponent } from './components/add-location/add-location.component';
import { ViewEventsComponent } from './components/view-events/view-events.component';
import { AddEventComponent } from './components/add-event/add-event.component';

const routes: Routes = [
  { path: '', redirectTo: '/authentication/login', pathMatch: 'full' },
  { path: 'app', component: HomePageComponent, canActivate: [appGuard()]},
  { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
  { path: 'app/view-locations', component: ViewLocationsComponent, canActivate: [adminGuard()]},
  { path: 'app/add-location', component: AddLocationComponent, canActivate: [adminGuard()]},
  { path: 'app/view-events', component: ViewEventsComponent},
  { path: 'app/add-event', component: AddEventComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
