import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  OnInit,
  Input,
  TemplateRef
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
export class CalendarComponent implements OnInit {
  @ViewChild('modalContent')
  modalContent: TemplateRef<any>;
  @Input() holidays;
  @Input() userVacationRequests;
  //holidays: Holiday[];
  //vacationRequests: UserVacationRequest[];

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  modalData: {
    action: string;
    event: CalendarEvent;
  };

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
      }
    }
  ];
  refresh: Subject<any> = new Subject();



  constructor(private modal: NgbModal, private holidayService: HolidayService, private vacationRequestService: VacationRequestService) { }
  ngOnInit() {
    //this.showAll();
    //this.showUserVacationRequest();


  }
  events: CalendarEvent[]=[];
  //showAll(): void {
  //  this.holidayService.showAll()
  //    .subscribe(holidays => this.holidays = holidays);
  //  console.log("Show active");
  //}
  //showUserVacationRequest(): void {
  //  this.vacationRequestService.showUserVacationRequest().subscribe(requests => this.vacationRequests = requests);
  //}
  createEvents(): void {
    this.events = [];  
    this.holidays.map((element) => {
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
    }).forEach(item => this.events.push(item));
    if (this.userVacationRequests != undefined) {
      this.userVacationRequests.map((element) => {
        var color: EventColor;
        if (element.status == "New") {
          color = colors.blue;
        }
        else if (element.status == "Declined") {
          color = colors.red;
        }
        else if (element.status == "Allowed") {
          color = colors.green;
        }
        return {
          start: new Date(element.startDate),
          end: new Date(element.endDate),
          title: element.vacationType,
          color: color,
          actions: this.actions,
          allDay: true,
          resizable: {
            beforeStart: true,
            afterEnd: true
          },
          draggable: true
        }
      }).forEach(itemV => this.events.push(itemV));
      this.refresh.next();
    }
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
}
