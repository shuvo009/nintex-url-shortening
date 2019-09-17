import { Component, OnInit } from '@angular/core';
import { ApiConstant, AppConstant } from '../helpers'
import { HttpClientService } from "../services/httpClient.service";
import { IShortUrlLog, IShortUrlModel } from "../models"
declare var $: any;

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.css'],
  providers: [HttpClientService],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  longUrl: string;
  shortUrlModels: IShortUrlModel[] = [];
  selectedShortUrlLog: IShortUrlLog[] = [];
  errorMessage: string;
  isOnRequest: boolean;
  baseUrl: string = window.location.origin;

  constructor(private httpClient: HttpClientService) {
  }

  ngOnInit() {
    this.getShortUrls();
  }

  async onGenerateClick() {
    this.errorMessage = '';
    try {
      this.inputGuard();
      const params = { longUrl: this.longUrl };
      await this.httpClient.postAsync(ApiConstant.createShortUrl, params);
      this.longUrl = '';
      this.getShortUrls();
    } catch (e) {
      this.errorMessage = e;
    }
  }

  async removeShortUrl(id: number) {
    this.errorMessage = '';
    try {
      const params = { id: id };
      await this.httpClient.postAsync(ApiConstant.removeShortUrl, params);
      this.getShortUrls();
    } catch (e) {
      this.errorMessage = e;
    }
  }

  getShortUrl(key: string) {
    return `${this.baseUrl}/r/${key}`;
  }

  async showLogs(id: number) {
    try {
      this.selectedShortUrlLog = [];
      $('#shortUrlLog').modal({
        show: true,
        backdrop: 'static',
        keyboard: false,
      });
      this.selectedShortUrlLog = await this.httpClient.getAsync(`${ApiConstant.getLogsOfShortUrl}\\${id}`);
    } catch (e) {
      this.errorMessage = e;
    }
  }

  //#region Supported Methods
  private async getShortUrls() {
    try {
      this.shortUrlModels = await this.httpClient.getAsync(ApiConstant.getShortUrls);
    } catch (e) {
      this.shortUrlModels = [];
    }
  }

  inputGuard() {
    if (!this.longUrl || this.longUrl.length < 1) {
      throw 'Please enter url';
    }
    const regExp = new RegExp(AppConstant.urlPatten);
    const isValid = regExp.test(this.longUrl);
    if (!isValid) {
      throw 'Please enter valid url';
    }
  }

  //#endregion
}
