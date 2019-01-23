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
@Component({
  selector: 'app-vacation-request',
  templateUrl: './vacation-request.component.html',
  styleUrls: ['./vacation-request.component.css'],
  providers: [DatePipe]
})
export class VacationRequestComponent implements OnInit, OnChanges {
  userVacationRequest: UserVacationRequest;
  //userVacationRequests: UserVacationRequest[];
  @Input() userVacationRequests;
  date: string;
  currentRole: any;
  allRoles;
  constructor(private vacationRequestService: VacationRequestService, private vacationPoliciesService: VacationPoliciesService, private datePipe: DatePipe) { }
  vacationTypes: VacationType[];
  ngOnChanges(changes: SimpleChange) {
    //this.showUserVacationRequest();
    this.date = this.datePipe.transform(new Date(), 'dd-MM-yy');
    console.log(changes);
  }
  ngOnInit() {
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.getVacationTypes();
    this.allRoles = Roles;
  }
  getVacationTypes(): void {
    this.vacationPoliciesService.getVacationTypes()
      .subscribe(types => this.vacationTypes = types);
  }
  createVacationRequest(vacationType: string, startDate: Date, endDate: Date) {  
    this.userVacationRequest = {
      id: 0,
      startDate: startDate,
      endDate: endDate,
      userId: localStorage.getItem('id'),
      userName:"",
      vacationType: vacationType,    
      status:"new" 
    } 
    this.vacationRequestService.createVacationRequest(this.userVacationRequest).subscribe(response => {

      if (response != null)
        this.userVacationRequests.push(this.userVacationRequest);  
    });  
  } 

  //showUserVacationRequest(): void {
  //  this.vacationRequestService.showUserVacationRequest() 
  //    .subscribe(vp => this.userVacationRequests = vp);        
  //}

  deleteVacationRequest(userVacationRequest: UserVacationRequest ): void {
    this.vacationRequestService.deleteUserVacationRequest(userVacationRequest); 
    var index = this.userVacationRequests.indexOf(userVacationRequest); 
    this.userVacationRequests.splice(index,1);
    //this.userVacationRequests = this.userVacationRequests.filter(h => h !== userVacationRequest);
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

