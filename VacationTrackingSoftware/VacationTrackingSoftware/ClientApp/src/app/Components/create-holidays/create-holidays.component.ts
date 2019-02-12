import { Component, OnInit } from '@angular/core';
import { Holiday } from '../../InterfacesAndClasses/Holiday';
import { HolidayService } from '../../Services/holiday.service';
import { MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-holidays',
  templateUrl: './create-holidays.component.html',
  styleUrls: ['./create-holidays.component.css']
})
export class CreateHolidaysComponent implements OnInit {
  holiday: Holiday;
  success: string;
  constructor(private holidayService: HolidayService, private dialogRef: MatDialogRef<CreateHolidaysComponent>, private router: Router) { }
  errors: string;
  ngOnInit() {
  }

  add(date: Date, name: string): void {
    name = name.trim();
    this.holiday = {
      id: 0,
      date: date,
      description: name
    }
    this.holidayService.addHoliday(this.holiday).subscribe(result => {
      if (result.successful == true) {
        this.success = "Congratulation!";
        this.errors = "";
        this.router.navigate(['/holidays']);
      }
      else {
        this.errors = result.errors[0];
        this.success = "";
      }

    },
      error => {
        if (error.status == 200) {
          this.success = error.error.text;
          this.router.navigate(['/holidays'])
        }
        else { this.errors = error.error.holidayError; }
      })
  };
  close() {
    this.dialogRef.close();
  }
}
