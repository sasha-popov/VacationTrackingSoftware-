import { Injectable } from '@angular/core';
import { HttpRequestService } from '.././Services/httpRequest.service'
import { Observable } from 'rxjs';
import { Team } from '../InterfacesAndClasses/Team'


@Injectable()
export class TeamService {
  
  constructor(private http: HttpRequestService) { }

  getAllTeams(): Observable<Team[]>{
    return this.http.get<Team[]>('api/Teams/GetAllTeams');
  }
}
