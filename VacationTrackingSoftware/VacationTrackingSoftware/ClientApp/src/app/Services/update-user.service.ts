import { Injectable } from '@angular/core';
import { HttpRequestService } from '.././Services/httpRequest.service'
import { UpdateUser } from '../InterfacesAndClasses/UpdateUser';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UpdateUserService {
  constructor(private http: HttpRequestService) { }

  getCurrentAppUser() {
    return this.http.get("api/UpdateUser/GetCurrentUser");
  }
  update(user: any) {
    return this.http.post("api/UpdateUser/Update", user);
  }

}
