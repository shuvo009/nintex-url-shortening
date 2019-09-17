import { HttpClientService } from "../services/httpClient.service"

export abstract class BaseAuthComponent {
  username: string = '';
  password: string = '';
  isOnRequest: boolean = false;

  errorMessage: string;

  abstract afterRequestComplete(data: any): void;

  abstract getRequestParam(): any;

  constructor(private api: string, private httpClient: HttpClientService) { }

  async onButtonClick() {
    this.errorMessage = null;
    this.isOnRequest = true;
    try {
      this.inputGuard();
      const params = this.getRequestParam();
      let response = await this.httpClient.postAsync(this.api, params);
      this.afterRequestComplete(response);
    } catch (e) {
      this.errorMessage = e;
    }
    this.isOnRequest = false;
  }

  inputGuard(): void {
    if (!this.username || this.username.length < 1) {
      throw 'Please enter username';
    }
    if (!this.password || this.password.length < 1) {
      throw 'Please enter password';
    }
  }

}
