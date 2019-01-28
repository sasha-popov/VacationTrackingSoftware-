import { Component, OnChanges, Input } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { error } from 'util';
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
  isVisible:string
  date: string;
  errors: string;
  constructor(private holidayService: HolidayService) { }

  ngOnChanges() {
    //this.date = this.datePipe.transform(new Date(), 'dd-MM-yy');
    //this.showAll();  
  }
  clickShowHolidays(): void {
    if (this.isVisible == "yes") {
      this.isVisible = "";
    }
    else {
      this.isVisible = "yes";
    }
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
    },
      error => {
        if (error.error.text == "Account created") {/*code for alert*/ }
        else { this.errors = error.error.holidayError; }
      })
    };
    //this.holidays.push(this.holiday);
  }
