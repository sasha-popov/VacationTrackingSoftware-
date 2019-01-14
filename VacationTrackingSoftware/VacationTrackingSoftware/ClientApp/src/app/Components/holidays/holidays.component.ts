import { Component, OnChanges, Input } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.css']
})
export class HolidaysComponent implements OnChanges {
  @Input() isHrUser;
 // holidays: Holiday[];
  holiday: Holiday;
  @Input() holidays; 
  constructor(private holidayService: HolidayService) { }

  ngOnChanges() {
    //this.showAll();
  }

  //showAll(): void {
  //  this.holidayService.showAll()
  //    .subscribe(holidays => this.holidays = holidays);
  //  //console.log(this.holidays.length);    
  //}

  deleteHoliday(holiday: Holiday): void {
    this.holidayService.deleteHoliday(holiday);
    this.holidays.splice(this.holidays.indexOf(holiday),1);
    //this.holidays = this.holidays.filter(h => h !== holiday);
  }

  add(date: Date, name: string): void {
    name = name.trim();
    //if (!name || !date) { return; }
    this.holiday = {
      id: 0,
      date: date,
      description: name
    }
    this.holidayService.addHoliday(this.holiday).subscribe();
    this.holidays.push(this.holiday);
  }

}
