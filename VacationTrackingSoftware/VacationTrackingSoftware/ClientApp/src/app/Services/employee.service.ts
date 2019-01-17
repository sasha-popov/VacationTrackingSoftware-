import { Injectable } from '@angular/core';
import { UserRegistration } from '../InterfacesAndClasses/UserRegistration'
import { HttpRequestService } from '.././Services/httpRequest.service'
import { Observable } from 'rxjs';


@Injectable()
export class EmployeeService {
  test = "ttt";
  constructor(private http: HttpRequestService) { }

  createEmployee(employee: UserRegistration): Observable<UserRegistration>{
    return this.http.post<UserRegistration>("api/Account/PostCreate", employee); 
  }
}
