import { Component, OnInit, Inject } from '@angular/core';
import { Holiday } from '../../InterfacesAndClasses/Holiday';
import { HolidayService } from '../../Services/holiday.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-holiday',
  templateUrl: './update-holiday.component.html',
  styleUrls: ['./update-holiday.component.css']
})
export class UpdateHolidayComponent implements OnInit {
  holiday: Holiday;
  constructor(private holidayService: HolidayService, private dialogRef: MatDialogRef<UpdateHolidayComponent>, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: Holiday) { }
  errors: string;
  success: string;
  ngOnInit() {
    this.data;
  }

  updateHoliday(date: Date, description: string): void {
    this.holiday = {
      id: this.data.id,
      date: date,
      description: description
    }
    this.holidayService.updateHoliday(this.holiday).subscribe(result => {
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
          this.errors = "";
          this.router.navigate(['/holidays']);
        }
        else {
          this.errors = error.error.holidayError;
          this.success = "";
        }
      })
  };
  close() {
    this.dialogRef.close();
  }
}
