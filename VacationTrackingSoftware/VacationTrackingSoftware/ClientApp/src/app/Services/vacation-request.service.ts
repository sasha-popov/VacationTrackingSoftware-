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
    return this.http.post<UserVacationRequest>("api/Employee/CreateVacationRequest", vacationRequest); 
  }
  showUserVacationRequest(): Observable<UserVacationRequest[]> {
    return this.http.get<UserVacationRequest[]>("api/Employee/ShowUserVacationRequest/");  
  }

  showUserVacationRequestForManager(): Observable<UserVacationRequest[]> {
    return this.http.get<UserVacationRequest[]>("api/Employee/ShowUserVacationRequestForManager");
  }

  deleteUserVacationRequest(userVacationRequest: UserVacationRequest){
    this.http.delete<UserVacationRequest>("api/Employee/deleteUserVacationRequest/" + userVacationRequest.startDate + "/" + userVacationRequest.endDate).subscribe();      
  }

  changeStatus(choose: boolean, id: number)
  {
    return this.http.post("api/Manager/ChangeStatus", JSON.stringify({ choose: choose, id: id }));  
  }
}
