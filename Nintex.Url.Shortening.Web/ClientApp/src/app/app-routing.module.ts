import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RouteConstant } from './helpers';

const routes: Routes = [
  { path: '', loadChildren: './home/home.module#HomeModule'},
  { path: RouteConstant.auth, loadChildren: './auth/auth.module#AuthModule' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
