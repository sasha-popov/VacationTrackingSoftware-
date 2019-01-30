import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  OnInit,
  Input,
  OnChanges,
  TemplateRef,
  SimpleChanges,
  SimpleChange
} from '@angular/core';
import { startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours } from 'date-fns';
import { Subject, Observable } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarView } from 'angular-calendar';
import { Holiday } from '../../InterfacesAndClasses/Holiday';
import { UserVacationRequest } from '../../InterfacesAndClasses/UserVacationRequest';
import { HolidayService } from '../../Services/holiday.service';
import { VacationRequestService } from '../../Services/vacation-request.service';
import { forEach } from '@angular/router/src/utils/collection';
import { map } from "rxjs/operators";
import { EventColor } from 'calendar-utils';
import { CalendarService } from '../../Services/CalendarService';
import { Roles } from '../../Enums/Roles';
import { DatePipe } from '@angular/common';
import { request } from 'http';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'    
  }, 
  green:{
    primary: '#14FF47',
    secondary: '#14FF47',
  } 

  
};
@Component({
  selector: 'mwl-demo-component',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['angular-calendar.css', 'flatpickr.css'],
  templateUrl: './calendar.component.html'
})
export class CalendarComponent implements OnInit, OnChanges {
  currentRole: any;
  @ViewChild('modalContent')
  modalContent: TemplateRef<any>;
  userVacationRequests: UserVacationRequest[];
  @Input() holidays;
  pipe = new DatePipe('en-US');
  
  ngOnChanges(changes: SimpleChanges) {
  }
  view: CalendarView = CalendarView.Month; 

  CalendarView = CalendarView;

  viewDate: Date = new Date(); 

  modalData: {
    action: string;
    event: CalendarEvent;
  };
  @Input() events: CalendarEvent[] = [];
  showAllHolidays(): void {
    this.holidayService.showAll()
      .subscribe(holidays => {
        this.holidays = holidays;
        this.addHolidayToEvents(this.holidays);
      })
  }

  actions: CalendarEventAction[] = [];
  refresh: Subject<any> = new Subject();

  constructor(private modal: NgbModal, private holidayService: HolidayService, private vacationRequestService: VacationRequestService, private calendarService:CalendarService) {
  }
  ngOnInit() {
    //this.showAllHolidays();
    //this.showUserVacationRequest();
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.showAllHolidays();
    if (parseInt(localStorage.getItem('rolesUser'), 10) != Roles.HrUser){
      this.showUserVacationRequest();
    }   //this.createEvents();
  }
  showUserVacationRequest(): void {
    if (parseInt(localStorage.getItem('rolesUser'), 10) == Roles.Manager) {
      this.vacationRequestService.showUserVacationRequestForManager().subscribe(requests => {
        this.userVacationRequests = requests;
        if (this.userVacationRequests != null) {
          this.addUserVacationRequestToEvents(this.userVacationRequests);
        }
      });
    }
    else if (parseInt(localStorage.getItem('rolesUser'), 10) == Roles.Employee) {
      this.vacationRequestService.showUserVacationRequest().subscribe(requests => {
        this.userVacationRequests = requests;
        if (this.userVacationRequests != null) {
          this.addUserVacationRequestToEvents(this.userVacationRequests);
        }
      });
    }
  }

  addHolidayToEvents(holidays: Holiday[]) {
    holidays.map((element) => {
      return {
        start: new Date(element.date),
        end: new Date(endOfDay(element.date)),
        title: element.description,
        color: colors.yellow,
        actions: this.actions,
        allDay: true,
        resizable: {
          beforeStart: true,
          afterEnd: true
        },
        draggable: true
      }
    }).forEach(item => {
      this.events.push(item);
      this.refresh.next();
      });
  }
  addUserVacationRequestToEvents(userVR: UserVacationRequest[]) {
    userVR.map((element) => {
      var description;
      if (this.currentRole == Roles.Employee) {
        description = element.vacationType;
      }
      else if (this.currentRole == Roles.Manager) {
        description = "Employee: " + element.userName + ", vacation type: " + element.vacationType + ", StartDate:" + this.pipe.transform(element.startDate) + ", and EndDate:" + this.pipe.transform(element.endDate) + ".";
      }
      var color: EventColor;
      if (element.status == "New") {
        color = colors.blue;
      }
      else if (element.status == "Declined") {
        color = colors.red;
      }
      else if (element.status == "Accepted") {
        color = colors.green;
      }
      return {
        start: new Date(element.startDate),
        end: new Date(element.endDate),
        title: description,
        color: color,
        actions: this.actions,
        allDay: true,
        resizable: {
          beforeStart: true,
          afterEnd: true
        },
        draggable: true
      }
    }).forEach(item => {
      this.events.push(item);
      this.refresh.next();
    });
  }
    activeDayIsOpen: boolean = true;
  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      this.viewDate = date;
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false; 
      } else {
        this.activeDayIsOpen = true;
      }
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void { 
    event.start = newStart;
    event.end = newEnd;
    this.handleEvent('Dropped or resized', event);
    this.refresh.next();
  }

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' }); 
  }

  addEvent(): void {

    this.events.push({
      title: 'New event',
      start: startOfDay(new Date()),
      end: endOfDay(new Date()),
      color: colors.red,
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true
      }
    });
    this.refresh.next();
  }


  //old method
  //createEvents(): void {
  //  this.events = [];
  //  if (this.holidays != undefined) {
  //    this.holidays.map((element) => {
  //      return {
  //        start: new Date(element.date),
  //        end: new Date(endOfDay(element.date)),
  //        title: element.description,
  //        color: colors.yellow,
  //        actions: this.actions,
  //        allDay: true,
  //        resizable: {
  //          beforeStart: true,
  //          afterEnd: true
  //        },
  //        draggable: true
  //      }
  //    }).forEach(item => this.events.push(item));
  //  }
  //  if (this.userVacationRequests != undefined) {
  //    var description;
  //    this.userVacationRequests.map((element) => {
  //      if (this.currentRole == Roles.Employee) {
  //        description = element.vacationType;
  //      }
  //      else if (this.currentRole == Roles.Manager) {
  //        description = "Employee: " + element.userName + ", vacation type: " + element.vacationType + ", StartDate:" + this.pipe.transform(element.startDate) + ", and EndDate:" + this.pipe.transform(element.endDate) + ".";
  //      }
  //      var color: EventColor;
  //      if (element.status == "New") {
  //        color = colors.blue;
  //      }
  //      else if (element.status == "Declined") {
  //        color = colors.red;
  //      }
  //      else if (element.status == "Accepted") {
  //        color = colors.green;
  //      }
  //      return {
  //        start: new Date(element.startDate),
  //        end: new Date(element.endDate),
  //        title: description,
  //        color: color,
  //        actions: this.actions,
  //        allDay: true,
  //        resizable: {
  //          beforeStart: true,
  //          afterEnd: true
  //        },
  //        draggable: true
  //      }
  //    }).forEach(itemV => this.events.push(itemV));
  //    this.refresh.next();
  //  }
  //}
}
