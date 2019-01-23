import { Component, OnChanges, Input } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'
import { map } from 'rxjs/operators';
//import { CalendarComponent } from '../../Components/calendar'

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

  deleteHoliday(holiday: Holiday): void {
    this.holidayService.deleteHoliday(holiday).subscribe();
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
    this.holidayService.addHoliday(this.holiday).subscribe(result => { 
      if (result != null)
        this.holidays.push(this.holiday);
      })
    };
    //this.holidays.push(this.holiday);
  }
