import { Component, OnInit } from '@angular/core';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { ActivatedRoute, Router } from "@angular/router";
import { HomeService } from '../../Services/home.service';
import { AuthorizeService } from '../../Services/authorize.service';
import { Roles } from '../../Roles';

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.css',],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  public constructor(private route: ActivatedRoute, private homeService: HomeService) { 
  }
  currentRole: any;
  allRoles;
  ngOnInit() {
    this.allRoles = Roles;
    this.currentRole = parseInt(localStorage.getItem('rolesUser'),10);
  }

  //fileNameDialogRef: MatDialogRef<CreateEmployeeComponent>;
  //openDialog() {
  //  const dialogConfig = new MatDialogConfig();
  //  dialogConfig.disableClose = true;
  //  //dialogConfig.autoFocus = true;  
  //  dialogConfig.hasBackdrop = true;
  //  //this.dialog.open(CreateEmployeeComponent, dialogConfig);
  //  let dialogRef = this.dialog.open(CreateEmployeeComponent, dialogConfig);
  //}

}
