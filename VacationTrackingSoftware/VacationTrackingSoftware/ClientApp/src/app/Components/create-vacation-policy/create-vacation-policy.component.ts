import { Component, OnInit, Input } from '@angular/core';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPolicy } from '../../InterfacesAndClasses/VacationPolicy';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateEmployeeComponent } from '../create-employee/create-employee.component';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-create-vacation-policy',
  templateUrl: './create-vacation-policy.component.html',
  styleUrls: ['./create-vacation-policy.component.css']
})
export class CreateVacationPolicyComponent implements OnInit {
  vacationPolicies: VacationPolicy[];
  vacationPolicy: VacationPolicy;
  vacationTypes: VacationType[];
  date: string;
  errors: string;
  success: string;
  constructor(private vacationPoliciesService: VacationPoliciesService, private route: ActivatedRoute, private dialogRef: MatDialogRef<CreateEmployeeComponent>, private router: Router) { }

  ngOnInit() {
    this.getVacationTypes();

  }
  getVacationTypes(): void {
    this.vacationPoliciesService.getVacationTypes()
      .subscribe(types => this.vacationTypes = types);
  }
  sendVacationPolicy(years: number, vacationType: string, count: number, payments: number): void {
    this.vacationPolicy = {
      id: 0,
      workingYear: years,
      vacationType: vacationType,
      count: count,
      payments: payments,
      userId: localStorage.getItem('id')
    }
    this.vacationPoliciesService.sendVacationPolicy(this.vacationPolicy).subscribe(vp => this.vacationPolicies.push(this.vacationPolicy), error => {
      if (error.status != 400) {
        this.success = error.error.text; this.errors = ""; this.router.navigate(['/vacationPolicies']);}
      else { this.errors = error.error.vacationPolicyError; this.success = "" }
    });
  }
  close() {
    this.dialogRef.close();
  }

}
