import { Component, OnChanges, Input, OnInit, SimpleChange } from '@angular/core';
import { UserVacationRequest } from '../../InterfacesAndClasses/UserVacationRequest';
import { VacationRequestService } from '../../Services/vacation-request.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { DatePipe } from '@angular/common';
import { Roles } from '../../Enums/Roles';
import { Response } from 'selenium-webdriver/http';
import { StatusesRequest } from '../../Enums/StatusesRequest';
import { Console } from '@angular/core/src/console';
import { error } from 'protractor';
import { CreateVacationRequestComponent } from '../create-vacation-request/create-vacation-request.component';
import { MatDialogRef, MatDialogConfig, MatDialog } from '@angular/material';
import { Router, NavigationEnd } from '@angular/router';
@Component({
  selector: 'app-vacation-request',
  templateUrl: './vacation-request.component.html',
  styleUrls: ['./vacation-request.component.css'],
})
export class VacationRequestComponent implements OnInit, OnChanges {
  userVacationRequest: UserVacationRequest;
  //userVacationRequests: UserVacationRequest[];
  @Input() userVacationRequests;
  date: string;
  currentRole: any;
  roles;
  errors: string;
  success: string;
  dateNow: Date = new Date();
  dateNowISO = this.dateNow.toISOString();
  constructor(private vacationRequestService: VacationRequestService, private vacationPoliciesService: VacationPoliciesService, private dialog: MatDialog, private router: Router) {
    this.router.events.subscribe((e: any) => {
      // If it is a NavigationEnd event re-initalise the component
      if (e instanceof NavigationEnd) {
        this.initialiseInvites();
      }
    });
  }

  initialiseInvites() {
    this.showUserVacationRequest();
  }

  ngOnChanges() {
  }
  ngOnInit() {
    //this.date = this.datePipe.transform(new Date(), 'dd-MM-yy');
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.roles = Roles;
    this.dateNowISO;
    this.showUserVacationRequest();
  }

  deleteVacationRequest(userVacationRequest: UserVacationRequest): void {
    this.vacationRequestService.deleteUserVacationRequest(userVacationRequest).subscribe(rez => {
      var index = this.userVacationRequests.indexOf(userVacationRequest);
      this.userVacationRequests.splice(index, 1);
    }); 

  }
  showUserVacationRequest(): void {
    if (parseInt(localStorage.getItem('rolesUser'), 10) == Roles.Manager) {
      this.vacationRequestService.showUserVacationRequestForManager().subscribe(requests => this.userVacationRequests = requests);
    }
    else if (parseInt(localStorage.getItem('rolesUser'), 10) == Roles.Employee) {
      this.vacationRequestService.showUserVacationRequest().subscribe(requests => this.userVacationRequests = requests);
    }
  }
  changeStatus(choose: boolean, id: number, userVacationRequest: UserVacationRequest): void {
    this.vacationRequestService.changeStatus(choose, id).subscribe(res => {
      if (res != null) {
        var index = this.userVacationRequests.indexOf(userVacationRequest);
        this.userVacationRequests.splice(index, 1);
        if (res["status"] == StatusesRequest.accepted)
          userVacationRequest.status = "Accepted";
        else
          userVacationRequest.status = "Declined";
        this.userVacationRequests.push(userVacationRequest);
      }
    });
  }
  fileNameDialogRef: MatDialogRef<CreateVacationRequestComponent>;
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    let dialogRef = this.dialog.open(CreateVacationRequestComponent, dialogConfig);
  }


  clickdeleteVacationRequest(name: string, userVacationRequest: UserVacationRequest) {
    if (confirm("Are you sure to " + name + " this request")) {
      this.deleteVacationRequest(userVacationRequest);
    }
  }

  clickMethodchangeStatus(name: string, choose: boolean, id: number, userVacationRequest: UserVacationRequest) {
    if (confirm("Are you sure to " + name + " this request")) {
      this.changeStatus(choose, id, userVacationRequest);
    }
  }


}

