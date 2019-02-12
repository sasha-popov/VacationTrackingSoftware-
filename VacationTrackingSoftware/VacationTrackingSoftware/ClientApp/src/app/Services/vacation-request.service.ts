import { Injectable } from '@angular/core';
import { UserVacationRequest } from '../InterfacesAndClasses/UserVacationRequest';
import { Observable } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service'
import { UserRole } from '../InterfacesAndClasses/UserRole';


@Injectable()
export class VacationRequestService {
  check: boolean;
  constructor(private http: HttpRequestService) { }

  createVacationRequest(vacationRequest: UserVacationRequest): Observable<UserVacationRequest> {
    return this.http.post<UserVacationRequest>("api/VacationRequest/CreateVacationRequest", vacationRequest); 
  }
  showUserVacationRequest(): Observable<UserVacationRequest[]> {
    return this.http.get<UserVacationRequest[]>("api/VacationRequest/ShowUserVacationRequest/");  
  }

  showUserVacationRequestForManager(): Observable<UserVacationRequest[]> {
    return this.http.get<UserVacationRequest[]>("api/VacationRequest/ShowUserVacationRequestForManager");
  }

  deleteUserVacationRequest(userVacationRequest: UserVacationRequest): Observable<any> {
    return this.http.delete<UserVacationRequest>("api/VacationRequest/deleteUserVacationRequest/" + userVacationRequest.id);      
  }

  changeStatus(choose: boolean, id: number)
  {
    return this.http.put("api/VacationRequest/ChangeStatus", JSON.stringify({ choose: choose, id: id }));  
  }
}
