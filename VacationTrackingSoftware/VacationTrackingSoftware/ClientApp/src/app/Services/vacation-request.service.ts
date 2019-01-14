import { Injectable } from '@angular/core';
import { UserVacationRequest } from '../InterfacesAndClasses/UserVacationRequest';
import { Observable } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service'


@Injectable()
export class VacationRequestService {

  constructor(private http: HttpRequestService) { }

  createVacationRequest(vacationRequest: UserVacationRequest): Observable<UserVacationRequest> {
    return this.http.post<UserVacationRequest>("api/Employee/CreateVacationRequest", vacationRequest); 
  }
  showUserVacationRequest(): Observable<UserVacationRequest[]> {
    return this.http.get<UserVacationRequest[]>("api/Employee/ShowUserVacationRequest/"+8);
  }

  deleteUserVacationRequest(userVacationRequest: UserVacationRequest): void {
    this.http.delete<UserVacationRequest>("api/Employee/deleteUserVacationRequest/" + userVacationRequest.startDate + "/" + userVacationRequest.endDate).subscribe();
  }
}
