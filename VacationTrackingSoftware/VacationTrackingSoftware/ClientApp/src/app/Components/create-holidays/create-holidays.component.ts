import { Component, OnInit } from '@angular/core';
import { Holiday } from '../../InterfacesAndClasses/Holiday';
import { HolidayService } from '../../Services/holiday.service';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-create-holidays',
  templateUrl: './create-holidays.component.html',
  styleUrls: ['./create-holidays.component.css']
})
export class CreateHolidaysComponent implements OnInit {
  holiday: Holiday;
  constructor(private holidayService: HolidayService, private dialogRef: MatDialogRef<CreateHolidaysComponent>) { }
  errors: string;
  ngOnInit() {
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
      //if (result != null)
      //  this.holidays.push(this.holiday);
    },
      error => {
        if (error.error.text == "Holiday created") {/*code for alert*/ }
        else { this.errors = error.error.holidayError; }
      })
  };
  close() {
    this.dialogRef.close();
  }
}
