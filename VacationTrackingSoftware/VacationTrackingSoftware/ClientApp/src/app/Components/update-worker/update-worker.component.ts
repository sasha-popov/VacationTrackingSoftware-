import { Component, OnInit, OnChanges, Inject } from '@angular/core';
import { UserRegistration, ManagerRegistration } from '../../InterfacesAndClasses/UserRegistration'
import { EmployeeService } from '../../Services/employee.service'
import { Team } from '../../InterfacesAndClasses/Team';
import { TeamService } from '../../Services/team.service'
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';
import { error } from '@angular/compiler/src/util';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogConfig } from "@angular/material";
import { Roles } from "../../Enums/Roles"

@Component({
  selector: 'app-update-worker',
  templateUrl: './update-worker.component.html',
  styleUrls: ['./update-worker.component.css']
})
export class UpdateWorkerComponent implements OnInit {
  roles;
  selectedRole: string;
  teams: Team[];
  check: boolean;
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  teamsId = [];
  selectedItem: number;
  errors: string;
  success: string;
  userName: string;
  constructor(private route: ActivatedRoute, private dialogRef: MatDialogRef<UpdateWorkerComponent>, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: any, private teamService: TeamService, private employeeService: EmployeeService) { }

  ngOnInit() {
    this.selectedRole = this.data.role;
    this.roles = Roles;
    this.getAllTeams();
    this.userName = this.data.firstName + " " + this.data.lastName;
  }
  close() {
    this.dialogRef.close();  
  }

  getAllTeams() {
    this.teamService.getAllTeams().subscribe(result => {
      this.teams = result
      if (this.data.role == Roles.Manager) {
        if (this.data.teams.length !== 0) this.selectedItems = this.data.teams;
        this.dropdownSettings = {
          singleSelection: false,
          idField: 'id',
          textField: 'name',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 3,
          allowSearchFilter: true
        }
      } else if (this.data.role == Roles.Employee) {
        if (this.data.team !== null) this.selectedItem = this.data.team.id;
        else this.selectedItem = 0;
      }
    });
  }
  updateUser(user: any) {
    this.teamsId = [];
    this.selectedItems.forEach(item => {
      this.teamsId.push(item.id)
    })
    this.employeeService.updateUser(user, this.selectedItem, this.teamsId).subscribe(result => {
      if (result.successful === true) {
        this.success = "Data have changed already!";
        this.errors = ""
        this.router.navigate(['/allWorkers']);
      }
      else {
        this.errors = "Data did not change.Please try later!";
        this.success=""
      }

      
    }),
      error => {
        this.errors = "Data did not change.Please try later!";
        this.success = ""
      }
  };
  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }
  }
