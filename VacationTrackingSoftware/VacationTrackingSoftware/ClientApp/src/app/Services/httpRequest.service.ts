import { HttpClient, HttpHeaders } from '@angular/common/http';
//import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

let authToken = localStorage.getItem('auth_token');
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
    ,'Authorization': `Bearer ${authToken}`})
};

@Injectable()
export class HttpRequestService {
  headers: HttpHeaders;
  constructor(private http: HttpClient) { }

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(url, httpOptions);
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(url, httpOptions);
  }

  post<T>(url: string, item: T): Observable<T> {
    return this.http.post<T>(url, item, httpOptions);
  }

}
httpOptions
