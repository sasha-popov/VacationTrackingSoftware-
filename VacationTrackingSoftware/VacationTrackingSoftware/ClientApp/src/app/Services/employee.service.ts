import { Injectable } from '@angular/core';
import { UserRegistration } from '../InterfacesAndClasses/UserRegistration'
import { HttpRequestService } from '.././Services/httpRequest.service'
import { Observable } from 'rxjs';


@Injectable()
export class EmployeeService {

  constructor(private http: HttpRequestService) { }

  createEmployee(employee: UserRegistration) {
    return this.http.post<UserRegistration>("api/Account/PostCreate", employee); 
  }
}
