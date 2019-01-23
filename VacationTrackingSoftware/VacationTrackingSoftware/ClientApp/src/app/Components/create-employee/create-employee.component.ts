import { Component, OnInit } from '@angular/core';
import { UserRegistration  } from '../../InterfacesAndClasses/UserRegistration'
import { EmployeeService } from '../../Services/employee.service'
import { Location } from '@angular/common';
import { Team } from 'src/app/InterfacesAndClasses/Team';
import { TeamService } from '../../Services/team.service'
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {
  employee: UserRegistration;
  teams: Team[];


  constructor(private employeeService: EmployeeService, private location: Location, private teamService: TeamService, private router: Router) { }

  ngOnInit() {
    this.getAllTeams();

  }
  CreateUser(name: string, surname: string, phoneNumber: string, email: string, password: string, role: string, teamId: number): void {
    this.employee = {
      firstName: name,
      lastName: surname,
      phoneNumber: phoneNumber,
      email: email,
      password: password,
      role: role,
      teamId: teamId
    }
    this.employeeService.createEmployee(this.employee).subscribe();
      this.router.navigate(['/']);    
  }
  getAllTeams() {
    this.teamService.getAllTeams().subscribe(result => this.teams = result); 
  }

  GoBack() {
    this.location.back();     
  }
}


