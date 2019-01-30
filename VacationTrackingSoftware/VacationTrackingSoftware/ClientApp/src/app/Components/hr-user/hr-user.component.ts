import { Component, OnInit, OnChanges } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'
import { MatDialog, MatDialogRef, MatDialogConfig  } from '@angular/material';
import { CreateEmployeeComponent } from '../create-employee/create-employee.component';
import { HeaderService } from '../../Services/HeaderService/header-service.service';
import { Operations } from '../../Operations';

@Component({
  selector: 'app-hr-user',
  templateUrl: './hr-user.component.html',
  styleUrls: ['./hr-user.component.css']
})
export class HrUserComponent implements OnInit, OnChanges {
  isActiveVacationPolice = true;
  operations;
  currentOperation: number;
  constructor(private holidayService: HolidayService, private dialog: MatDialog, private headerService: HeaderService) { } 
  ngOnInit() {
    this.showAll();
    this.operations = Operations;
  }
  ngOnChanges() {
    this.currentOperation = this.headerService.currentOperation;
  }

  isHrUser = true;
  holidays: Holiday[];
  showAll(): void {
    this.holidayService.showAll()
      .subscribe(holidays => this.holidays = holidays);
    //console.log(this.holidays.length);    
  }

  //fileNameDialogRef: MatDialogRef<CreateEmployeeComponent>;
  //openDialog() {
  //  const dialogConfig = new MatDialogConfig();
  //  dialogConfig.disableClose = true;
  //  //dialogConfig.autoFocus = true;  
  //  dialogConfig.hasBackdrop = true;
  //  //this.dialog.open(CreateEmployeeComponent, dialogConfig);
  //    let dialogRef = this.dialog.open(CreateEmployeeComponent, dialogConfig);
  //}
}
