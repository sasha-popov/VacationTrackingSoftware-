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
  StatusesRequest = StatusesRequest;
  userVacationRequest: UserVacationRequest;
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
    this.showUserVacationRequest();
  }
  ngOnInit() {
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.roles = Roles;
    this.dateNowISO;
  }

  deleteVacationRequest(userVacationRequest: UserVacationRequest): void {
    this.vacationRequestService.deleteUserVacationRequest(userVacationRequest).subscribe(result => {
      if (result.successful == false) {
        this.refresh(result.errors[0]);
      }
      var index = this.userVacationRequests.indexOf(userVacationRequest);
      this.userVacationRequests.splice(index, 1);
    });
    }

  refresh(describe: string) {
    if (confirm(describe + " Just refresh page.")) {
    }
  }
  showUserVacationRequest(): void {
    if (parseInt(localStorage.getItem('rolesUser'), 10) == Roles.Manager) {
      this.vacationRequestService.showUserVacationRequestForManager().subscribe(requests => {
        this.userVacationRequests = requests;
        var re = StatusesRequest[requests[0].status];
      });
    }
    else if (parseInt(localStorage.getItem('rolesUser'), 10) == Roles.Employee) {
      this.vacationRequestService.showUserVacationRequest().subscribe(requests => {
        this.userVacationRequests = requests;
      });
    }
  }
  changeStatus(choose: boolean, id: number, userVacationRequest: UserVacationRequest): void {
    this.vacationRequestService.changeStatus(choose, id).subscribe(res => {
      if (res != null) {
        var index = this.userVacationRequests.indexOf(userVacationRequest);
        this.userVacationRequests.splice(index, 1);
        this.router.navigate(['/vacationRequests']);
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

