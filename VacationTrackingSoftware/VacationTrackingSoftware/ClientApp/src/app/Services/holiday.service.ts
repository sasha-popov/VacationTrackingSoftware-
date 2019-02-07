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

  deleteHoliday(holiday: Holiday){
    return this.http.delete<Holiday>("api/Holiday/DeleteHoliday/" + holiday.description + "/" + holiday.date);
  }


  addHoliday(holiday: Holiday) {
    return this.http.post<Holiday>("api/Holiday/AddHoliday", holiday);  
  }

  updateHoliday(holiday: Holiday) {
    return this.http.post<Holiday>("api/Holiday/UpdateHoliday", holiday);
  }

}
