import { Component, OnInit, OnChanges, Inject } from '@angular/core';
import { UserRegistration, ManagerRegistration } from '../../InterfacesAndClasses/UserRegistration'
import { EmployeeService } from '../../Services/employee.service'
import { Location } from '@angular/common';
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
  constructor(private route: ActivatedRoute, private dialogRef: MatDialogRef<UpdateWorkerComponent>, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: any, private teamService: TeamService) { }

  ngOnInit() {
    this.getAllTeams();
  }
  close() {
    this.dialogRef.close();
  }

  getAllTeams() {
    this.teamService.getAllTeams().subscribe(result => {
      this.teams = result
      if (this.data.role = Roles.Manager) {
        this.selectedItems = this.data.teams;
      }
      this.dropdownSettings = {
        singleSelection: false,
        idField: 'id',
        textField: 'name',
        selectAllText: 'Select All',
        unSelectAllText: 'UnSelect All',
        itemsShowLimit: 3,
        allowSearchFilter: true
      };
      this.selectedRole = this.data.role;
      this.roles = Roles;
    });
  }
}
