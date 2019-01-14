import { Component, OnInit } from '@angular/core';
import { Employee } from '../../InterfacesAndClasses/Employee'
import { EmployeeService } from '../../Services/employee.service'
import { Location } from '@angular/common';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {
  employee: Employee;

  constructor(private employeeService: EmployeeService, private location: Location) { }

  ngOnInit() {
    this.employeeService.createEmployee
  }
  CreateEmployee(name: string, surname: string, phoneNumber: string, email: string): void {
    this.employee = {
      name: name,
      surname: surname,
      phoneNumber: phoneNumber,
      email: email
    }
    this.employeeService.createEmployee(this.employee);
    this.location.back();
  }

  GoBack() {
    this.location.back();
  }
}


