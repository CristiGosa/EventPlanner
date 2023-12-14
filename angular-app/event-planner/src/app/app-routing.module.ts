import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { appGuard } from './shared/guards/app.guard';

const routes: Routes = [
  { path: '', redirectTo: '/authentication/login', pathMatch: 'full' },
  { path: 'app', component: HomePageComponent, canActivate: [appGuard()]},
  { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
