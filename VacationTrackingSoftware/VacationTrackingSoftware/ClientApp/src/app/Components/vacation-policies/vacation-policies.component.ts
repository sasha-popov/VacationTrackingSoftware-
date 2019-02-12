import { Component, OnInit, Input, Inject } from '@angular/core';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPolicy } from '../../InterfacesAndClasses/VacationPolicy';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { MatDialogRef, MatDialog, MatDialogConfig } from '@angular/material';
import { CreateVacationPolicyComponent } from '../create-vacation-policy/create-vacation-policy.component';
import { Roles } from '../../Enums/Roles';
import { error } from 'util';
import { AuthorizeService } from '../../Services/authorize.service'
import { AuthorizeComponent } from '../authorize/authorize.component';
import { MAT_DIALOG_DATA } from '@angular/material';
import { UpdateVacationPolicyComponent } from '../update-vacation-policy/update-vacation-policy.component';
import { window } from 'rxjs/operators';


@Component({
  selector: 'app-vacation-policies',
  templateUrl: './vacation-policies.component.html',
  styleUrls: ['./vacation-policies.component.css'],

})
export class VacationPoliciesComponent implements OnInit {
  currentRole: any;
  roles;
  vacationPolicies: VacationPolicy[];
  vacationPolicy: VacationPolicy;
  vacationTypes: VacationType[];
  errors: string;
  success: string;

  constructor(private vacationPoliciesService: VacationPoliciesService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private auth: AuthorizeService,
    private router: Router) {
    this.router.events.subscribe((e: any) => {
      // If it is a NavigationEnd event re-initalise the component
      if (e instanceof NavigationEnd) {
        this.initialiseInvites();
      }
    });
  }

  initialiseInvites() {
    this.showAll();
  }


  ngOnInit() {
    this.showAll();
    this.getVacationTypes();
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.roles = Roles;
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
  update(vacationPolicy: VacationPolicy) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    dialogConfig.data = vacationPolicy;
    let dialogRef = this.dialog.open(UpdateVacationPolicyComponent, dialogConfig);
  }

  showAll(): void {
    this.vacationPoliciesService.showAll()
      .subscribe(vp => {
        this.vacationPolicies = vp;
      }, error => {
        if (error.status = 401) {
          this.auth.logout();
          this.router.navigate(['/login']);
        }
      });
  }

  deleteVacationPolicy(vacationPolisy: VacationPolicy) {
    this.vacationPoliciesService.deleteVacationPolicy(vacationPolisy).subscribe(result => {
      if (result.successful == false) {
        this.refresh(result.errors[0]);
      }
    });
    this.vacationPolicies = this.vacationPolicies.filter(h => h !== vacationPolisy);
  }
  refresh(describe: string) {
    if (confirm(describe + " Just refresh page.")) {
    }
  }

  clickdeleteVacationRequest(name: string, vacationPolisy: VacationPolicy) {
    if (confirm("Are you sure to " + name + " this policy?")) {
      this.deleteVacationPolicy(vacationPolisy);
    }
  }

}
