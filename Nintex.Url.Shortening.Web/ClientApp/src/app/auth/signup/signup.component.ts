import { Component, OnInit } from '@angular/core';
import { BaseAuthComponent } from "../base.auth.component"
import { ApiConstant } from '../../helpers'
import { HttpClientService } from "../../services/httpClient.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  providers: [HttpClientService],
  styleUrls: ['./signup.component.css']
})
export class SignupComponent extends BaseAuthComponent {

  confirmPassword: string;
  name: string;

  constructor(httpClient: HttpClientService, private router: Router) {
    super(ApiConstant.signup, httpClient);
  }

  getRequestParam() {
    return {
      username: this.username,
      password: this.password,
      confirmPassword: this.confirmPassword,
      name: this.name
    }
  }


  afterRequestComplete(data): void {
    this.router.navigate(['/auth/login']);
  }

  inputGuard(): void {
    super.inputGuard();
    if (!this.confirmPassword || this.confirmPassword.length < 1) {
      throw 'Please enter confirm password';
    }

    if (!this.name || this.name.length < 1) {
      throw 'Please enter name';
    }
  }

}
