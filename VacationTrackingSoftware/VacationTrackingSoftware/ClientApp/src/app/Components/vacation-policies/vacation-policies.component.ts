import { Component, OnInit, Input } from '@angular/core';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPolicy } from '../../InterfacesAndClasses/VacationPolicy';
import { ActivatedRoute } from '@angular/router';
import { MatDialogRef, MatDialog, MatDialogConfig } from '@angular/material';
import { CreateVacationPolicyComponent } from '../create-vacation-policy/create-vacation-policy.component';
import { Roles } from '../../Roles';


@Component({
  selector: 'app-vacation-policies',
  templateUrl: './vacation-policies.component.html',
  styleUrls: ['./vacation-policies.component.css'],
  
})
export class VacationPoliciesComponent implements OnInit {
  currentRole: any;
  allRoles;
  vacationPolicies: VacationPolicy[];
  vacationPolicy: VacationPolicy;
  vacationTypes: VacationType[];
  date: string;
  errors: string;
  success: string;
  constructor(private vacationPoliciesService: VacationPoliciesService, private route: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit() {
    this.showAll();
    this.getVacationTypes(); 
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.allRoles = Roles;
  }
  getVacationTypes(): void {
    this.vacationPoliciesService.getVacationTypes()  
      .subscribe(types => this.vacationTypes = types);  
  }

  fileNameDialogRef: MatDialogRef<CreateVacationPolicyComponent>;
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    let dialogRef = this.dialog.open(CreateVacationPolicyComponent, dialogConfig);
  }

  showAll(): void {
    this.vacationPoliciesService.showAll()
      .subscribe(vp => this.vacationPolicies = vp); 
  }

  deleteVacationPolicy(vacationPolisy: VacationPolicy): void {
    this.vacationPoliciesService.deleteVacationPolicy(vacationPolisy); 
    this.vacationPolicies = this.vacationPolicies.filter(h => h !== vacationPolisy);   
  }  
}
