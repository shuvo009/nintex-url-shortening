import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from "./app.component";
import { LoaderService, LoaderInterceptor, AuthGuard } from "./helpers";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { AppRoutingModule } from "./app-routing.module"

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [
    AuthGuard,
    LoaderService,
    LoaderInterceptor,
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
