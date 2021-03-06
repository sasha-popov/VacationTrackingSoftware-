import { Injectable } from '@angular/core';
import { Holiday } from '../InterfacesAndClasses/Holiday';
import { Observable } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service'
import {tap } from 'rxjs/operators';


@Injectable()
export class HolidayService {
  public holiday: Holiday;
  constructor(private http: HttpRequestService) { }
  showAll(): Observable<Holiday[]> {
    return this.http.get<Holiday[]>("api/Holiday/GetForCurrentYear");
  }

  deleteHoliday(holiday: Holiday): void{
    this.http.delete<Holiday>("api/Holiday/DeleteHoliday/" + holiday.description + "/" + holiday.date);
  }

  addHoliday(holiday: Holiday): Observable<Holiday> {
   return this.http.post<Holiday>("api/Holiday/AddHoliday", holiday); 
  }

}
