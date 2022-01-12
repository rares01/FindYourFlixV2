import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import {AuthenticationService} from "./authentication.service";
import {Observable} from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  intercept(request: HttpRequest<any>, newRequest: HttpHandler): Observable<HttpEvent<any>> {

    return newRequest.handle(request).pipe(catchError(err =>{
      if (err.status === 401) {
        this.authenticationService.logout();
      }
      return Observable.throw(err.error.message || err.statusText);
    }));
  }
}
