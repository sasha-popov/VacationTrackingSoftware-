import { Injectable } from '@angular/core';
import { UserData } from '../InterfacesAndClasses/UserData';
import { Observable } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service';
import { UserRole, Role, User } from '.././InterfacesAndClasses/UserRole';
import { Router, NavigationExtras } from "@angular/router";

@Injectable()
export class AuthorizeService {
  constructor(private http: HttpRequestService, private router: Router) {
  }

  chekUser(User: UserData): Observable<UserRole> {
    return this.http.get<UserRole>('api/Account/Redirect/' + User.name + '/' + User.password);
  }
}
