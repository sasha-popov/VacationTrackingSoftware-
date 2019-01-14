import { Injectable } from '@angular/core';
import { VacationPolicy } from '../InterfacesAndClasses/VacationPolicy';
import { VacationType } from '../InterfacesAndClasses/VacationType';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable()
export class VacationPoliciesService {
  vacationPolicies: VacationPolicy[];
  vacationType: VacationType[];
  constructor(private http: HttpClient) { }

  getVacationTypes(): Observable<VacationType[]> {
    return this.http.get<VacationType[]>("api/VacationPolicies/GetTypesOfVacation");
  }

  sendVacationPolicy(vacationPolicy: VacationPolicy): Observable<VacationPolicy> {
    return this.http.post<VacationPolicy>("api/VacationPolicies/SendVacationPolicy", vacationPolicy, httpOptions);
  }

  showAll(): Observable<VacationPolicy[]> {
    return this.http.get<VacationPolicy[]>("api/VacationPolicies/GetVacationPolicies");
  }

  deleteVacationPolicy(vacationPolicy: VacationPolicy): void {
    this.http.delete<VacationPolicy>("api/VacationPolicies/DeleteVacationPolicy/" + vacationPolicy.workingYear + "/" + vacationPolicy.vacationType + "/" + vacationPolicy.payments, httpOptions).subscribe();
  }
}
