import { Injectable } from '@angular/core';
import { UserVacationRequest } from '../InterfacesAndClasses/UserVacationRequest';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable()
export class VacationRequestService {

  constructor(private http: HttpClient) { }

  createVacationRequest(vacationRequest: UserVacationRequest): Observable<UserVacationRequest> {
    return this.http.post<UserVacationRequest>("api/Employee/CreateVacationRequest", vacationRequest, httpOptions); 
  }
  showUserVacationRequest(): Observable<UserVacationRequest[]> {
    return this.http.get<UserVacationRequest[]>("api/Employee/ShowUserVacationRequest/"+8);
  }

  deleteUserVacationRequest(userVacationRequest: UserVacationRequest): void {
    this.http.delete<UserVacationRequest>("api/Employee/deleteUserVacationRequest/" + userVacationRequest.startDate + "/" + userVacationRequest.endDate, httpOptions).subscribe();
  }
}
