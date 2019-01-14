import { Injectable } from '@angular/core';
import { UserData } from '../InterfacesAndClasses/UserData';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class AuthorizeService {
  constructor(private http: HttpClient) { }

  chekUser(User: UserData): void {
    this.http.post('api/Account/Redirect', User, httpOptions).subscribe();
  }
}
