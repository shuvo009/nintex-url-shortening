import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { AppConstant } from "./appConstant";

@Injectable()
export class LoaderService {
  private isLoading = new Subject<boolean>();
  setHttpStatus(inFlight: boolean) {
    this.isLoading.next(inFlight);
  }
  getHttpStatus(): Observable<boolean> {
    return this.isLoading.asObservable();
  }
}

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
  constructor(public loaderService: LoaderService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loaderService.setHttpStatus(true);
    let token = localStorage.getItem(AppConstant.authToken);
    if (token) {
      req = req.clone({
        setHeaders: {
          'Authorization': token
        },
      });
    }
    return next.handle(req).pipe(
      finalize(() => this.loaderService.setHttpStatus(false))
    );
  }
}
