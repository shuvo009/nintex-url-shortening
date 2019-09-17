import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home.component";
import { AuthGuard } from '../helpers';

const routes: Routes = [
  //{ path: '', component: HomeComponent, canActivate: [AuthGuard] }
  { path: '', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule {
}
