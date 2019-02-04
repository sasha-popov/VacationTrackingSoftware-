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

@Component({
  selector: 'app-update-worker',
  templateUrl: './update-worker.component.html',
  styleUrls: ['./update-worker.component.css']
})
export class UpdateWorkerComponent implements OnInit {
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
  success: string;
  constructor(private route: ActivatedRoute, private dialogRef: MatDialogRef<UpdateWorkerComponent>, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }
  close() {
    this.dialogRef.close();
  }
}
