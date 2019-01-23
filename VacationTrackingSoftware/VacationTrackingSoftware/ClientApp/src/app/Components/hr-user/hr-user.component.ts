import { Component, OnInit } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'

@Component({
  selector: 'app-hr-user',
  templateUrl: './hr-user.component.html',
  styleUrls: ['./hr-user.component.css']
})
export class HrUserComponent implements OnInit {

  constructor(private holidayService: HolidayService) { } 
  ngOnInit() {
    this.showAll();
  }
  isHrUser = true;
  holidays: Holiday[];
  showAll(): void {

    this.holidayService.showAll()
      .subscribe(holidays => this.holidays = holidays);
    //console.log(this.holidays.length);    
  }
}
