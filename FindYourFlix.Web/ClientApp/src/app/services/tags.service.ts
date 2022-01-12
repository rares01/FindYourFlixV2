import {Inject, Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Tag} from "../models/tag";

@Injectable()
export class TagsService {
  http: HttpClient;

  constructor(@Inject(HttpClient) http: any) {
    this.http = http;
  }

  insert(tag: Tag): Observable<string> {
    return this.http.post('https://localhost:5001/Tag', tag, {responseType: 'text'});
  }

  delete(id: string) {
    return this.http.delete(`https://localhost:5001/Tag/${id}`);
  }
}
