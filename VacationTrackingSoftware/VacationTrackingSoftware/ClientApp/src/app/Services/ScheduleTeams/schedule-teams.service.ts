import { Injectable } from '@angular/core';
import { HttpRequestService } from '../../Services/httpRequest.service'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScheduleTeamsService {

  constructor(private http: HttpRequestService) { }
  showAll(): Observable<any[]> {
    return this.http.get<any[]>("api/Teams/GetTeamsForManager");
  }
}
