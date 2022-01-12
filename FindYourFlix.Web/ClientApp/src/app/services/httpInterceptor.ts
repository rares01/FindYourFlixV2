import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class httpInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, newRequest: HttpHandler): Observable<HttpEvent<any>> {
    if(localStorage.getItem('TokenInfo')) {
      let tokenInfo = JSON.parse(localStorage.getItem('TokenInfo') || '');
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${tokenInfo.token}`,
          'Content-Type': 'application/json; charset=utf8',
          'Accept': 'application/json'
        }
      });
    }

    return newRequest.handle(request);
  }
}
