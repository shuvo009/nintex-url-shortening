import { Component, OnInit } from '@angular/core';
import { BaseAuthComponent } from "../base.auth.component"
import { ApiConstant, AppConstant } from '../../helpers'
import { HttpClientService } from "../../services/httpClient.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [HttpClientService],
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseAuthComponent {

  constructor(httpClient: HttpClientService, private router: Router) {
    super(ApiConstant.login, httpClient);
  }

  getRequestParam() {
    return {
      username: this.username,
      password: this.password,
    }
  }

  afterRequestComplete(data): void {
    localStorage.setItem(AppConstant.authToken, data.token);
    localStorage.setItem(AppConstant.authName, data.name);
    this.router.navigate(['/']);
  }

}
