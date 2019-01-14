import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { AuthorizeComponent } from './Components/authorize/authorize.component';
import { HrUserComponent } from './Components/hr-user/hr-user.component';
import { CreateEmployeeComponent } from './Components/create-employee/create-employee.component';
import { HolidaysComponent } from './Components/holidays/holidays.component';
import { VacationPoliciesComponent } from './Components/vacation-policies/vacation-policies.component';
import { VacationRequestComponent } from './Components/vacation-request/vacation-request.component';
import { EmployeeComponent } from './Components/employee/employee.component';

import { AuthorizeService } from './Services/authorize.service';
import { EmployeeService } from './Services/employee.service';
import { HolidayService } from './Services/holiday.service';
import { VacationPoliciesService } from './Services/vacation-policies.service';
import { VacationRequestService } from './Services/vacation-request.service';  

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { CalendarComponent } from './Components/calendar/calendar.component';
import { HttpRequestService } from './Services/httpRequest.service'

import { FlatpickrModule } from 'angularx-flatpickr';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';        
  

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AuthorizeComponent,
    HrUserComponent,
    CreateEmployeeComponent,
    HolidaysComponent,
    VacationPoliciesComponent,
    EmployeeComponent,
    VacationRequestComponent, 
    CalendarComponent  
  ],
  imports: [
    NgbModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FlatpickrModule.forRoot(),
    RouterModule.forRoot([   
      { path: 'hrUser', component: HrUserComponent },
      { path: 'createEmployee', component: CreateEmployeeComponent },
      { path: 'employee', component: EmployeeComponent },
      { path: 'calendar', component: CalendarComponent },
      { path: 'login', component: AuthorizeComponent },
      { path: '', redirectTo: '/login', pathMatch:'full' }
    ]), 
    BrowserAnimationsModule,
    CalendarModule.forRoot({
     provide: DateAdapter,
     useFactory: adapterFactory 
    })
  ],
  providers: [AuthorizeService,
    EmployeeService,
    HolidayService,
    VacationPoliciesService,
    VacationRequestService,
    HttpRequestService
  ],

  bootstrap: [AppComponent],
  //exports: [CalendarComponent]
})
export class AppModule { }  
