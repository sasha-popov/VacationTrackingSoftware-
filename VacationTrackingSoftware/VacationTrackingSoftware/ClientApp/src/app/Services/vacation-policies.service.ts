import { Injectable } from '@angular/core';
import { VacationPolicy } from '../InterfacesAndClasses/VacationPolicy';
import { VacationType } from '../InterfacesAndClasses/VacationType';
import { Observable } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service'

@Injectable()
export class VacationPoliciesService {
  vacationPolicies: VacationPolicy[];
  vacationType: VacationType[];
  constructor(private http: HttpRequestService) { }

  getVacationTypes(): Observable<VacationType[]> {
    return this.http.get<VacationType[]>("api/VacationPolicies/GetTypesOfVacation");
  }

  sendVacationPolicy(vacationPolicy: VacationPolicy): Observable<VacationPolicy> {
    return this.http.post<VacationPolicy>("api/VacationPolicies/SendVacationPolicy", vacationPolicy);
  }

  updateVacationPolicy(vacationPolicy: VacationPolicy): Observable<VacationPolicy> {
    return this.http.put<VacationPolicy>("api/VacationPolicies/UpdateVacationPolicy", vacationPolicy);
  }
  showAll(): Observable<VacationPolicy[]> {
    return this.http.get<VacationPolicy[]>("api/VacationPolicies/GetVacationPolicies");
  }

  deleteVacationPolicy(vacationPolicy: VacationPolicy): Observable<any> {
    return this.http.delete<VacationPolicy>("api/VacationPolicies/DeleteVacationPolicy/" + vacationPolicy.id);
  }
}
