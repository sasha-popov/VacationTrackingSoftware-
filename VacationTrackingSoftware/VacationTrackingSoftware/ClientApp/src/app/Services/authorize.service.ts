import { Injectable } from '@angular/core';
import { UserData } from '../InterfacesAndClasses/UserData';
import { Observable } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service'



@Injectable()
export class AuthorizeService {
  constructor(private http: HttpRequestService) { }

  chekUser(User: UserData): void {
    this.http.post('api/Account/Redirect', User).subscribe();
  }
}
