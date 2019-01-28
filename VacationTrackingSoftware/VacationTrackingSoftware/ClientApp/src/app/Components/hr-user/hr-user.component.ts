import { Component, OnInit } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'
import { MatDialog, MatDialogRef, MatDialogConfig  } from '@angular/material';
import { CreateEmployeeComponent } from '../create-employee/create-employee.component';

@Component({
  selector: 'app-hr-user',
  templateUrl: './hr-user.component.html',
  styleUrls: ['./hr-user.component.css']
})
export class HrUserComponent implements OnInit {

  constructor(private holidayService: HolidayService, private dialog: MatDialog) { } 
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

  fileNameDialogRef: MatDialogRef<CreateEmployeeComponent>;
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    //dialogConfig.autoFocus = true;  
    dialogConfig.hasBackdrop = true;
    //this.dialog.open(CreateEmployeeComponent, dialogConfig);
      let dialogRef = this.dialog.open(CreateEmployeeComponent, dialogConfig);
  }
}
