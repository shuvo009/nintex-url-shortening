import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from "./home.component";
import { HomeRoutingModule } from "./home-routing.module";
import { NavMenuComponent } from "../nav-menu/nav-menu.component";

@NgModule({
  imports: [
    CommonModule,
    HomeRoutingModule
  ],
  declarations: [HomeComponent, NavMenuComponent]
})
export class HomeModule { }
