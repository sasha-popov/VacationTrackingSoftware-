import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AuthorizeComponent } from './Components/authorize/authorize.component';
import { HrUserComponent } from './Components/hr-user/hr-user.component';
import { CreateEmployeeComponent } from './Components/create-employee/create-employee.component';
import { HolidaysComponent } from './Components/holidays/holidays.component';
import { VacationPoliciesComponent } from './Components/vacation-policies/vacation-policies.component';
import { VacationRequestComponent } from './Components/vacation-request/vacation-request.component';
import { EmployeeComponent } from './Components/employee/employee.component';
import { HomeComponent } from './Components/home/home.component'

import { AuthorizeService } from './Services/authorize.service';
import { EmployeeService } from './Services/employee.service';
import { HolidayService } from './Services/holiday.service';
import { VacationPoliciesService } from './Services/vacation-policies.service';
import { VacationRequestService } from './Services/vacation-request.service';
import { BaseService } from './Services/BaseService';
import { HomeService } from './Services/home.service';
 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { CalendarComponent } from './Components/calendar/calendar.component';
import { HttpRequestService } from './Services/httpRequest.service';
import { ConfigService } from './Services/ConfigService';
import { CalendarService } from './Services/CalendarService';
import { TeamService } from './Services/team.service';

import { FlatpickrModule } from 'angularx-flatpickr';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthGuard } from './Services/auth-guard';
import { ManagerComponent } from './Components/manager/manager.component';
import { AppHeaderComponent } from './Components/app-header/app-header.component';
import { ScheduleTeamsComponent } from './Components/schedule-teams/schedule-teams.component';
import { MatDialogModule } from '@angular/material';
import { TestPopupComponent } from './Components/test-popup/test-popup.component';
  

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
    CalendarComponent, ManagerComponent, AppHeaderComponent, ScheduleTeamsComponent, TestPopupComponent
  ],
  imports: [
    NgbModule,
    NgMultiSelectDropDownModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FlatpickrModule.forRoot(),
    RouterModule.forRoot([
      { path: 'hrUser', component: HrUserComponent },
      { path: 'createUser', component: CreateEmployeeComponent },
      { path: 'employee', component: EmployeeComponent },
      { path: 'calendar', component: CalendarComponent },
      { path: 'login', component: AuthorizeComponent },
      { path: '', redirectTo: '/login', pathMatch: 'full' },
      {
        path: 'home',
        canActivate: [AuthGuard],
        component: HomeComponent
      },
      { path: 'testPopup', component: TestPopupComponent}
    ]), 
    BrowserAnimationsModule,
    NgMultiSelectDropDownModule.forRoot(),
    CalendarModule.forRoot({
     provide: DateAdapter,
     useFactory: adapterFactory 
    }),
    MatDialogModule

  ],
  providers: [AuthorizeService,
    EmployeeService,
    HolidayService,
    VacationPoliciesService,
    VacationRequestService,
    HttpRequestService,
    HomeService,
    ConfigService,
    AuthGuard,
    CalendarService,
    TeamService
  ],

  bootstrap: [AppComponent],
  //exports: [CalendarComponent]
  entryComponents: [CreateEmployeeComponent]
})
export class AppModule { }  
