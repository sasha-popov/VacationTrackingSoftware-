import { Injectable } from '@angular/core';
import { HttpRequestService } from './httpRequest.service';

@Injectable({
  providedIn: 'root'
})
export class allWorkerService {

  constructor(private http: HttpRequestService) { }

  showAll() {
   return this.http.get("api/Teams/GetALLWorkersForHrUser");
  }
}
