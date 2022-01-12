import {Inject, Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {User} from "../models/user";

@Injectable()
export class UsersService {
  http: HttpClient;

  constructor(@Inject(HttpClient) http: any) {
    this.http = http;
  }

  get(): Observable<any> {
    return this.http.get(`https://localhost:5001/User`);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>('https://localhost:5001/User/list')
  }

  like(movieId: string){
    return this.http.post(`https://localhost:5001/User/${movieId}/like`, movieId);
  }

  put(model: User) {
    return this.http.put(`https://localhost:5001/User`, model);
  }

  register(model: User) {
    return this.http.post(`https://localhost:5001/User`, model);
  }

  updatePassword(oldPassword: string, newPassword: string): Observable<boolean> {
     return this.http.put<boolean>(`https://localhost:5001/User/update-password`, {oldPassword, newPassword});
  }

  updateRole(userId: string) {
    return this.http.put('https://localhost:5001/User/update-role', {userId: userId});
  }
}
