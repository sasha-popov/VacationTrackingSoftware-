import { Injectable } from '@angular/core';
import { Holiday } from '../InterfacesAndClasses/Holiday';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {tap } from 'rxjs/operators';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable()
export class HolidayService {
  public holiday: Holiday;
  constructor(private http: HttpClient) { }
  showAll(): Observable<Holiday[]> {
    return this.http.get<Holiday[]>("api/Holiday/GetForCurrentYear");
  }

  deleteHoliday(holiday: Holiday): void{
    this.http.delete<Holiday>("api/Holiday/DeleteHoliday/" + holiday.description + "/" + holiday.date, httpOptions).subscribe();
  }

  addHoliday(holiday: Holiday): Observable<Holiday> {
   return this.http.post<Holiday>("api/Holiday/AddHoliday", holiday, httpOptions); 
  }

}
