import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AuthorizeComponent } from './Components/authorize/authorize.component';
import { CreateEmployeeComponent } from './Components/create-employee/create-employee.component';
import { HolidaysComponent } from './Components/holidays/holidays.component';
import { VacationPoliciesComponent } from './Components/vacation-policies/vacation-policies.component';
import { VacationRequestComponent } from './Components/vacation-request/vacation-request.component';
import { HomeComponent } from './Components/home/home.component'

import { AuthorizeService } from './Services/authorize.service';
import { EmployeeService } from './Services/employee.service';
import { HolidayService } from './Services/holiday.service';
import { VacationPoliciesService } from './Services/vacation-policies.service';
import { VacationRequestService } from './Services/vacation-request.service';
 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { CalendarComponent } from './Components/calendar/calendar.component';
import { HttpRequestService } from './Services/httpRequest.service';
import { ConfigService } from './Services/ConfigService';
import { TeamService } from './Services/team.service';
import { allWorkerService } from "./Services/allWorkerService";

import { FlatpickrModule } from 'angularx-flatpickr';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthGuard } from './Services/auth-guard';
import { AppHeaderComponent } from './Components/app-header/app-header.component';
import { ScheduleTeamsComponent } from './Components/schedule-teams/schedule-teams.component';
import { MatDialogModule, MatTabsModule } from '@angular/material';
//import { MatTabsModule } from '@angular/material/tabs';
import { CreateVacationPolicyComponent } from './Components/create-vacation-policy/create-vacation-policy.component';
import { HeaderService } from './Services/HeaderService/header-service.service';
import { CreateHolidaysComponent } from './Components/create-holidays/create-holidays.component';
import { CreateVacationRequestComponent } from './Components/create-vacation-request/create-vacation-request.component';
import { UpdateVacationPolicyComponent } from './Components/update-vacation-policy/update-vacation-policy.component';
import { UpdateHolidayComponent } from './Components/update-holiday/update-holiday.component';
import { FilterVacationRequestPipe } from './Pipes/filter-vacationrequest.pipe';
import { ViewAllWorkersComponent } from './Components/view-all-workers/view-all-workers.component';
import { UpdateWorkerComponent } from './Components/update-worker/update-worker.component';
import { FilterListOfUsersPipe } from './Pipes/filter-listOfUsers.pipe';
  

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AuthorizeComponent,
    CreateEmployeeComponent,
    HolidaysComponent,
    VacationPoliciesComponent,
    VacationRequestComponent,
    CalendarComponent,
    AppHeaderComponent,
    ScheduleTeamsComponent,
    CreateVacationPolicyComponent,
    CreateHolidaysComponent,
    CreateVacationRequestComponent,
    UpdateVacationPolicyComponent,
    UpdateHolidayComponent,
    FilterVacationRequestPipe,
    ViewAllWorkersComponent,
    UpdateWorkerComponent,
    FilterListOfUsersPipe
  ],
  imports: [
    NgbModule,
    NgMultiSelectDropDownModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FlatpickrModule.forRoot(),
    MatTabsModule,
    RouterModule.forRoot([
      { path: 'calendar', canActivate: [AuthGuard], component: CalendarComponent, runGuardsAndResolvers: 'always' },
      { path: 'login', component: AuthorizeComponent },
      { path: '', redirectTo: '/login', canActivate: [AuthGuard], pathMatch: 'full' },
      { path: 'vacationPolicies', canActivate: [AuthGuard], component: VacationPoliciesComponent, runGuardsAndResolvers: 'always' },
      { path: 'vacationPolicies/:id', canActivate: [AuthGuard], component: UpdateVacationPolicyComponent, runGuardsAndResolvers: 'always' },
      { path: 'vacationRequests', canActivate: [AuthGuard], component: VacationRequestComponent, runGuardsAndResolvers: 'always' },
      { path: 'holidays', component: HolidaysComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always' },
      { path: 'holidays/:id', component: UpdateHolidayComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always' },
      { path: 'allWorkers', component: ViewAllWorkersComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always' },
      {
        path: 'home',
        canActivate: [AuthGuard],
        component: HomeComponent
      },
      { path: 'yourTeams', component: ScheduleTeamsComponent, canActivate: [AuthGuard]}
    ], {onSameUrlNavigation:'reload'}), 
    BrowserAnimationsModule,
    NgMultiSelectDropDownModule.forRoot(),
    CalendarModule.forRoot({
     provide: DateAdapter,
     useFactory: adapterFactory 
    }),
    MatDialogModule,
  ],
  providers: [AuthorizeService,
    EmployeeService,
    HolidayService,
    VacationPoliciesService,
    VacationRequestService,
    HttpRequestService,
    ConfigService,
    AuthGuard,
    TeamService,
    HeaderService,
    allWorkerService
  ],

  bootstrap: [AppComponent],
  //exports: [CalendarComponent]
  entryComponents: [CreateEmployeeComponent,
    CreateVacationPolicyComponent,
    CreateHolidaysComponent,
    CreateVacationRequestComponent,
    UpdateVacationPolicyComponent,
    UpdateHolidayComponent,
    UpdateWorkerComponent
  ]
})
export class AppModule { }  
