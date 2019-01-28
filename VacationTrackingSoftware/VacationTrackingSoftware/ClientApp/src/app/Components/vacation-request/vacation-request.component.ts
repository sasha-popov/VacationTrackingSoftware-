import { Component, OnChanges, Input, OnInit, SimpleChange } from '@angular/core';
import { UserVacationRequest } from '../../InterfacesAndClasses/UserVacationRequest';
import { VacationRequestService } from '../../Services/vacation-request.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { DatePipe } from '@angular/common';
import { Roles } from '../../Roles';
import { Response } from 'selenium-webdriver/http';
import { StatusesRequest } from '../../StatusesRequest';
import { Console } from '@angular/core/src/console';
import { error } from 'protractor';
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
  allRoles;
  errors: string;
  success: string;
  dateNow: Date = new Date();
  dateNowISO = this.dateNow.toISOString();
  constructor(private vacationRequestService: VacationRequestService, private vacationPoliciesService: VacationPoliciesService) { }
  vacationTypes: VacationType[];
  ngOnChanges() {
    //this.showUserVacationRequest();
  }
  ngOnInit() {
    //this.date = this.datePipe.transform(new Date(), 'dd-MM-yy');
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.getVacationTypes();
    this.allRoles = Roles;
    this.dateNowISO;
  }
  getVacationTypes(): void {
    this.vacationPoliciesService.getVacationTypes()
      .subscribe(types => this.vacationTypes = types);
  }
  createVacationRequest(vacationType: string, startDate: Date, endDate: Date): void {  
    this.userVacationRequest = {
      id: 0,
      startDate: startDate,
      endDate: endDate,
      userId: localStorage.getItem('id'),
      userName: localStorage.getItem('name'),
      vacationType: vacationType,    
      status: "new",
      payment: 0
    } 
    this.vacationRequestService.createVacationRequest(this.userVacationRequest).subscribe(rez => {
      //need to fix that
      //this.userVacationRequest.payment = parseInt(rez);
      this.errors = ""; this.userVacationRequests.push(rez);
    }, error => {
      if (error.status = 400) { }
      else { this.errors = error.error.vacationRequestError; this.success=""}
    });  
  } 

  deleteVacationRequest(userVacationRequest: UserVacationRequest): void {
    this.vacationRequestService.deleteUserVacationRequest(userVacationRequest).subscribe(rez => {
      var index = this.userVacationRequests.indexOf(userVacationRequest);
      this.userVacationRequests.splice(index, 1);
    }); 

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
}

