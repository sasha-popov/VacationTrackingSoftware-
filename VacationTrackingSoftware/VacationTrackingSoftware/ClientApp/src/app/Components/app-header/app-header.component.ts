import { Component, OnInit, Input } from '@angular/core';
import { AuthorizeService } from '../../Services/authorize.service';
import { Router } from '@angular/router';
import { Roles } from '../../Enums/Roles';
import { MatDialog, MatDialogRef, MatDialogConfig } from '@angular/material';
import { CreateEmployeeComponent } from '../create-employee/create-employee.component';
import { HeaderService } from '../../Services/HeaderService/header-service.service';


@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent implements OnInit {
  userName: string;
  currentRole: number;
  roles;
  pathImages: any = require("../../Images/schedule.png");
  constructor(private authorizeService: AuthorizeService, private router: Router, private dialog: MatDialog) { }
  ngOnInit() {
    this.userName = localStorage.getItem('name');
    this.roles = Roles;
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
  }

  logOut() {
    this.authorizeService.logout();
    this.router.navigate(['/login']);
  }


  fileNameDialogRef: MatDialogRef<CreateEmployeeComponent>;
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    let dialogRef = this.dialog.open(CreateEmployeeComponent, dialogConfig);
  }



}
