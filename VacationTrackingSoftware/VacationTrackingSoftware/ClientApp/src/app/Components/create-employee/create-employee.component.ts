import { Component, OnInit } from '@angular/core';
import { UserRegistration  } from '../../InterfacesAndClasses/UserRegistration'
import { EmployeeService } from '../../Services/employee.service'
import { Location } from '@angular/common';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {
  employee: UserRegistration ;

  constructor(private employeeService: EmployeeService, private location: Location) { }

  ngOnInit() {
  }
  CreateEmployee(name: string, surname: string, phoneNumber: string, email: string, password: string, role: string): void {
    this.employee = {
      firstName: name,
      lastName: surname,
      phoneNumber: phoneNumber,
      email: email,
      password: password,
      role: role
      //location: "Ukraine",
    }
    this.employeeService.createEmployee(this.employee).subscribe();   
    //this.location.back();
  }

  GoBack() {
    this.location.back();
  }
}


