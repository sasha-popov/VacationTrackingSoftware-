import { Component, OnChanges, Input, OnInit } from '@angular/core';
import { HolidayService } from '../../Services/holiday.service';
import { Holiday } from '../../InterfacesAndClasses/Holiday'
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { Roles } from '../../Enums/Roles'
import { CreateHolidaysComponent } from '../create-holidays/create-holidays.component';
import { MatDialogRef, MatDialogConfig, MatDialog } from '@angular/material';
import { UpdateHolidayComponent } from '../update-holiday/update-holiday.component';
import { NavigationEnd, Router } from '@angular/router';
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
  constructor(private holidayService: HolidayService, private dialog: MatDialog, private router: Router) {
    this.router.events.subscribe((e: any) => {
      // If it is a NavigationEnd event re-initalise the component
      if (e instanceof NavigationEnd) {
        this.initialiseInvites();
      }
    });
  }

  initialiseInvites() {
    this.showAll();
  }
  ngOnInit() {
    this.roles = Roles;
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.showAll();  
  }

  showAll(): void {
    this.holidayService.showAll()
      .subscribe(holidays => this.holidays = holidays);   
  }
  deleteHoliday(holiday: Holiday): void {
    this.holidayService.deleteHoliday(holiday).subscribe();
    this.holidays.splice(this.holidays.indexOf(holiday),1);
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
  update(holiday: Holiday) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    dialogConfig.data = holiday;
    let dialogRef = this.dialog.open(UpdateHolidayComponent, dialogConfig);
  }
  }
