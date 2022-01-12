import {Inject, Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Movie} from "../models/movie";

@Injectable()
export class MoviesService {
  http: HttpClient;

  constructor(@Inject(HttpClient) http: any) {
    this.http = http;
  }

  getList(): Observable<any> {
    return this.http.get('https://localhost:5001/Movie');
  }

  insert(movie: any) {
    return this.http.post('https://localhost:5001/Movie', movie);
  }
}
