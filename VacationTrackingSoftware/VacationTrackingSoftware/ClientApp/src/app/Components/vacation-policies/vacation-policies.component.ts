import { Component, OnInit, Input } from '@angular/core';
import { VacationPoliciesService } from '../../Services/vacation-policies.service';
import { VacationType } from '../../InterfacesAndClasses/VacationType';
import { VacationPolicy } from '../../InterfacesAndClasses/VacationPolicy';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-vacation-policies',
  templateUrl: './vacation-policies.component.html',
  styleUrls: ['./vacation-policies.component.css'],
  
})
export class VacationPoliciesComponent implements OnInit {
  @Input() isHrUser: string;
  @Input() isVisible: string;
  vacationPolicies: VacationPolicy[];
  vacationPolicy: VacationPolicy;
  vacationTypes: VacationType[];
  date: string;

  clickShowVacationPolicies(): void { 
    if (this.isVisible == "yes") {
      this.isVisible = "";
    }
    else {
      this.isVisible = "yes";
    }
  }
  constructor(private vacationPoliciesService: VacationPoliciesService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.showAll();
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
    this.vacationPoliciesService.sendVacationPolicy(this.vacationPolicy).subscribe(vp => this.vacationPolicies.push(this.vacationPolicy));   
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
