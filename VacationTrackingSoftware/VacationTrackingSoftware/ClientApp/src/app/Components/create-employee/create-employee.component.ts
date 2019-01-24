import { Component, OnInit, OnChanges } from '@angular/core';
import { UserRegistration, ManagerRegistration  } from '../../InterfacesAndClasses/UserRegistration'
import { EmployeeService } from '../../Services/employee.service'
import { Location } from '@angular/common';
import { Team } from '../../InterfacesAndClasses/Team';
import { TeamService } from '../../Services/team.service'
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit{
  employee: UserRegistration;
  manager: ManagerRegistration;
  selectedRole: string;
  teams: Team[];
  check: boolean;
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  teamsId = [];
  selectedItem: number;
  errors: string;
  constructor(private employeeService: EmployeeService, private location: Location, private teamService: TeamService, private router: Router) {

  }
  ngOnInit() {
    this.getAllTeams();
    console.log(this.teams);
  }
  CreateUser(name: string, surname: string, phoneNumber: string, email: string, password: string, role: string): void {
    this.errors = '';
    this.check = this.checkDate(name, surname, phoneNumber, email, password, role);
    if (this.check == true) {
      if (this.selectedItems.length == 0) {
        this.employee = {
          firstName: name,
          lastName: surname,
          phoneNumber: phoneNumber,
          email: email,
          password: password,
          role: role,
          teamId: this.selectedItem
        }
        this.employeeService.createEmployee(this.employee).subscribe(res => {
          this.router.navigate(['/']);
        },
          error => this.errors = error.error.registration);
      }
      else {
        this.selectedItems.forEach(item => {
          this.teamsId.push(item.id)
        })
        
        this.manager = {
          firstName: name,
          lastName: surname,
          phoneNumber: phoneNumber,
          email: email,
          password: password,
          role: role,
          teamsId: this.teamsId
        }
        this.employeeService.createManager(this.manager).subscribe(res => {
          this.router.navigate(['/']);
        },
          error => this.errors = error.error.registration);
      }


    } 
  }

  checkDate(name: string, surname: string, phoneNumber: string, email: string, password: string, role: string): boolean {
    if (name == "" || surname == "" || phoneNumber == "" || email == "" || password == "" || role == "") { return false }
    else { return true }
  }
  getAllTeams() {
    this.teamService.getAllTeams().subscribe(result => {
      this.teams = result
        this.selectedItems = [
        ];
        this.dropdownSettings = {
          singleSelection: false,
          idField: 'id',
          textField: 'name',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 3,
          allowSearchFilter: true
        };
    }); 
  }

  GoBack() { 
    this.location.back();     
  }

  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }
}


