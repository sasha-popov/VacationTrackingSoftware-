import { Injectable } from '@angular/core';
import { UserRegistration, ManagerRegistration } from '../InterfacesAndClasses/UserRegistration'
import { HttpRequestService } from '.././Services/httpRequest.service'
import { Observable } from 'rxjs';


@Injectable()
export class EmployeeService {
  test = "ttt";
  constructor(private http: HttpRequestService) { }
  createEmployee(employee: UserRegistration){
    return this.http.post<UserRegistration>("api/Account/PostCreateEmployee", employee); 
  }
  createManager(personalData: ManagerRegistration) {
    return this.http.post("api/Account/PostCreateManager", personalData);
  }
}
