import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class HttpClientService {

  constructor(private httpClient: HttpClient) { }

  postAsync(api: string, body: any): Promise<any> {
    return new Promise((resolve, reject) => {
      this.httpClient.post(api, body).subscribe((data) => {
        resolve(data);
      },
        (error) => {
          reject(error);
        });
    });
  }

  getAsync(api: string): Promise<any> {
    return new Promise((resolve, reject) => {
      this.httpClient.get(api).subscribe((data) => {
        resolve(data);
      },
        (error) => {
          reject(error);
        });
    });
  }
}
