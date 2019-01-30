import { Router } from '@angular/router';
import { Roles } from './Roles';
import { MatDialog, MatDialogRef, MatDialogConfig } from '@angular/material';
import { CreateEmployeeComponent } from './Components/create-employee/create-employee.component';
import { Component, OnInit, Input } from '@angular/core';
import { AuthorizeService } from './Services/authorize.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  @Input() title: string;
  userName: string;
  currentRole: number;
  roles;
  isActiveVacationPolicy: boolean;
  constructor(private authorizeService: AuthorizeService, private router: Router, private dialog: MatDialog) { }
  ngOnInit() {
    this.userName = localStorage.getItem('name');
    this.roles = Roles;
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.isActiveVacationPolicy = true;
  }

  logOut() {
    this.authorizeService.logout();
    this.router.navigate(['/']);
  }

  fileNameDialogRef: MatDialogRef<CreateEmployeeComponent>;
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    //dialogConfig.autoFocus = true;  
    dialogConfig.hasBackdrop = true;
    //this.dialog.open(CreateEmployeeComponent, dialogConfig);
    let dialogRef = this.dialog.open(CreateEmployeeComponent, dialogConfig);
  }
}
