import { Component, OnChanges, Input, OnInit } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { Roles } from '../../Enums/Roles'
import { CreateHolidaysComponent } from '../create-holidays/create-holidays.component';
import { MatDialogRef, MatDialogConfig, MatDialog } from '@angular/material';
//import { CalendarComponent } from '../../Components/calendar'

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.css']
})
export class HolidaysComponent implements OnInit {
  roles;
  currentRole;
  holidays: Holiday[];
  holiday: Holiday;
  date: string;
  errors: string;
  constructor(private holidayService: HolidayService,private dialog: MatDialog) { }

  ngOnInit() {
    //this.date = this.datePipe.transform(new Date(), 'dd-MM-yy');
    this.roles = Roles;
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.showAll();  
  }

  showAll(): void {
    this.holidayService.showAll()
      .subscribe(holidays => this.holidays = holidays);
    //console.log(this.holidays.length);    
  }
  deleteHoliday(holiday: Holiday): void {
    this.holidayService.deleteHoliday(holiday).subscribe();
    this.holidays.splice(this.holidays.indexOf(holiday),1);
    //this.holidays = this.holidays.filter(h => h !== holiday);
  }

  clickdeleteHoliday(name: string, holiday: Holiday) {
    if (confirm("Are you sure to " + name + " this holiday?")) {
      this.deleteHoliday(holiday);
    }
  }

  fileNameDialogRef: MatDialogRef<CreateHolidaysComponent>;
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    let dialogRef = this.dialog.open(CreateHolidaysComponent, dialogConfig);
  }
    //this.holidays.push(this.holiday);
  }
