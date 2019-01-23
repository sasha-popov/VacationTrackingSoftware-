import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  OnInit,
  Input,
  OnChanges,
  TemplateRef,
  SimpleChanges
} from '@angular/core';
import { startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours } from 'date-fns';
import { Subject, Observable } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarView } from 'angular-calendar';
import { Holiday } from '../InterfacesAndClasses/Holiday';
import { UserVacationRequest } from '../InterfacesAndClasses/UserVacationRequest';
import { HolidayService } from '../Services/holiday.service';
import { VacationRequestService } from '../Services/vacation-request.service';
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
  green: {
    primary: '#14FF47',
    secondary: '#14FF47',
  }


};
export class CalendarService {

  createEvents() {
    //events: CalendarEvent[] = [];
  }
}
