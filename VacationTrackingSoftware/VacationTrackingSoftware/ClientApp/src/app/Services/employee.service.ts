import { Injectable } from '@angular/core';
import { UserRegistration, ManagerRegistration } from '../InterfacesAndClasses/UserRegistration'
import { HttpRequestService } from '.././Services/httpRequest.service'
import { Observable, observable } from 'rxjs';
import { Roles } from '../Enums/Roles'
import { Role } from '../InterfacesAndClasses/UserRole';


@Injectable()
export class EmployeeService {
  constructor(private http: HttpRequestService) { }
  createEmployee(employee: UserRegistration){
    return this.http.post<UserRegistration>("api/Worker/Create", employee); 
  }
  createManager(personalData: ManagerRegistration) {
    return this.http.post("api/Manager/Create", personalData);
  }

  updateUser(user: any, teamId?: number, teamIds?: number[]): Observable<any> {
    return this.http.put("api/Teams/UpdateUserTeam", { userId: user.id, teamId: teamId, teamIds: teamIds, role: user.role });
    }
}
