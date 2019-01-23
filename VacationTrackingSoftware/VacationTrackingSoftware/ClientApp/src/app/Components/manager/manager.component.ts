import { Component, OnInit } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday';
import { VacationRequestService } from '../../Services/vacation-request.service';
import { UserVacationRequest } from '../../InterfacesAndClasses/UserVacationRequest';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.css']
})
export class ManagerComponent implements OnInit {
  constructor(private holidayService: HolidayService, private vacationRequestService: VacationRequestService) { }

  ngOnInit() {
    this.showAll();
    this.showUserVacationRequest();
  }
  holidays: Holiday[];
  userVacationRequests: UserVacationRequest[];
  showAll(): void {
    this.holidayService.showAll()
      .subscribe(holidays => this.holidays = holidays);
    //console.log(this.holidays.length);    
  }
  showUserVacationRequest(): void {
    this.vacationRequestService.showUserVacationRequestForManager().subscribe(requests => this.userVacationRequests = requests); 
  }
}
