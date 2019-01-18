import { Component, OnChanges, Input, OnInit } from '@angular/core';
import { UserVacationRequest } from '../../InterfacesAndClasses/UserVacationRequest';
import { VacationRequestService } from '../../Services/vacation-request.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-vacation-request',
  templateUrl: './vacation-request.component.html',
  styleUrls: ['./vacation-request.component.css'],
  providers: [DatePipe]
})
export class VacationRequestComponent implements OnChanges, OnInit {
  userVacationRequest: UserVacationRequest;
  //userVacationRequests: UserVacationRequest[];
  @Input() userVacationRequests;
  date: string;
  constructor(private vacationRequestService: VacationRequestService, private vacationPoliciesService: VacationPoliciesService, private datePipe: DatePipe) { }
  vacationTypes: VacationType[];
  ngOnChanges() {

    //this.showUserVacationRequest();
    this.date = this.datePipe.transform(new Date(), 'dd-MM-yy');
  }
  ngOnInit() {
    this.getVacationTypes();
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
      user: 8,
      vacationType: vacationType,    
      status:"new"
    } 
    this.vacationRequestService.createVacationRequest(this.userVacationRequest).subscribe(vr => this.userVacationRequests.push(this.userVacationRequest));  
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
}

