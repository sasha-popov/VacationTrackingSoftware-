import { Injectable } from '@angular/core';
import { Holiday } from '../InterfacesAndClasses/Holiday';
import { Observable } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service'
import {tap } from 'rxjs/operators';


@Injectable()
export class HolidayService {
  constructor(private http: HttpRequestService) { }
  showAll(): Observable<Holiday[]> {
    return this.http.get<Holiday[]>("api/Holiday/GetForCurrentYear");
  }

  deleteHoliday(holidayId: number) {
    return this.http.delete<any>("api/Holiday/DeleteHoliday/" + holidayId);
  }


  addHoliday(holiday: Holiday) {
    return this.http.post<any>("api/Holiday/AddHoliday", holiday);  
  }

  updateHoliday(holiday: Holiday) {
    return this.http.put<any>("api/Holiday/UpdateHoliday", holiday);
  }

}
