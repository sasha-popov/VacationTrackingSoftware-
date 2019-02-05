import { Injectable } from '@angular/core';
import { UserRegistration, ManagerRegistration } from '../InterfacesAndClasses/UserRegistration'
import { HttpRequestService } from '.././Services/httpRequest.service'
import { Observable } from 'rxjs';
import { Roles } from '../Enums/Roles'


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

  updateUser(userId: string, teamId?: number, teamsId?: number[]) {
    return this.http.post("api/Account/UpdateUserTeam", { userId: userId, teamId: teamId,teamsId:teamsId });
  }
}
