import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Employee } from '../InterfacesAndClasses/Employee'


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable()
export class EmployeeService {

  constructor( private http: HttpClient) { }

  createEmployee(empployee: Employee): void {
    this.http.post<Employee>("api/Account/CreateEmployee", empployee, httpOptions).subscribe();          
  }
}
