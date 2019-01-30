import { Component, OnInit } from '@angular/core';
import { UserVacationRequest } from '../../InterfacesAndClasses/UserVacationRequest';
import { VacationRequestService } from '../../Services/vacation-request.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { Roles } from '../../Roles';
import { MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-vacation-request',
  templateUrl: './create-vacation-request.component.html',
  styleUrls: ['./create-vacation-request.component.css']
})
export class CreateVacationRequestComponent implements OnInit {
  vacationTypes: VacationType[];
  currentRole: any;
  userVacationRequests: UserVacationRequest[];
  userVacationRequest: UserVacationRequest;
  user
  date: string;
  roles;
  errors: string;
  success: string;
  dateNow: Date = new Date();
  constructor(private vacationRequestService: VacationRequestService, private vacationPoliciesService: VacationPoliciesService, private dialogRef: MatDialogRef<CreateVacationRequestComponent>, private router: Router) { }

  ngOnInit() {
    this.getVacationTypes();
   this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.roles = Roles;
  }
  getVacationTypes(): void {
    this.vacationPoliciesService.getVacationTypes()
      .subscribe(types => this.vacationTypes = types);
  }
  close() {
    this.dialogRef.close();
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
      this.router.navigate(['/home']);
      this.dialogRef.close();
    }, error => {
      if (error.status = 400) { }
      else {
        this.errors = error.error.vacationRequestError; this.success = "";
        this.router.navigate(['/home']);
        this.dialogRef.close();}
    });
  } 

}
