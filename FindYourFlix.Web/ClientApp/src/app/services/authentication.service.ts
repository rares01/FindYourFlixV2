import {Inject, Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {
  http: HttpClient;

  constructor(@Inject(HttpClient) http: any) {
    this.http = http;
  }

  login(username: string, password: string) {
    return this.http.post<any>('https://localhost:5001/Login', { username, password })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('TokenInfo', JSON.stringify(user));
        }
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('TokenInfo');
  }
}
