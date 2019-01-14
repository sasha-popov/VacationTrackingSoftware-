import { Injectable } from '@angular/core';
import { Employee } from '../InterfacesAndClasses/Employee'
import { HttpRequestService } from '.././Services/httpRequest.service'


@Injectable()
export class EmployeeService {

  constructor(private http: HttpRequestService) { }

  createEmployee(employee: Employee): void {
    this.http.post<Employee>("api/Account/CreateEmployee", employee).subscribe();
  }
}
