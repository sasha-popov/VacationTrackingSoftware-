import { Component, OnInit, Input, Inject } from '@angular/core';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPolicy } from '../../InterfacesAndClasses/VacationPolicy';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateEmployeeComponent } from '../create-employee/create-employee.component';
import { MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-update-vacation-policy',
  templateUrl: './update-vacation-policy.component.html',
  styleUrls: ['./update-vacation-policy.component.css']
})
export class UpdateVacationPolicyComponent implements OnInit {
  vacationPolicy: VacationPolicy;
  vacationTypes: VacationType[];
  errors: string;
  success: string;
  constructor(private vacationPoliciesService: VacationPoliciesService, private route: ActivatedRoute, private dialogRef: MatDialogRef<UpdateVacationPolicyComponent>, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: VacationPolicy) { }

  ngOnInit() {
    this.getVacationTypes();

  }
  getVacationTypes(): void {
    this.vacationPoliciesService.getVacationTypes()
      .subscribe(types => this.vacationTypes = types);
  }
  updateVacationPolicy(id: number, years: number, vacationType: string, count: number, payments: number): void {
    this.vacationPolicy = {
      id: id,
      workingYear: years,
      vacationType: vacationType,
      count: count,
      payments: payments,
      userId: localStorage.getItem('id')
    }
    this.vacationPoliciesService.updateVacationPolicy(this.vacationPolicy).subscribe(vp => { }
      , error => {
      if (error.status != 400) {
        this.success = error.error.text; this.errors = ""; this.router.navigate(['/vacationPolicies']);
      }
      else { this.errors = error.error.vacationPolicyError; this.success = "" }
    });
  }
  close() {
    this.dialogRef.close();
  }


}
