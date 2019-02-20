import { Component, OnInit } from '@angular/core';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { ActivatedRoute, Router } from "@angular/router";
import { AuthorizeService } from '../../Services/authorize.service';
import { Roles } from '../../Enums/Roles';
import { HolidayService } from '../../Services/holiday.service';

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.css',],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  public constructor(private route: ActivatedRoute, private holidayService: HolidayService) { 
  }
  currentRole: any;
  roles;
  ngOnInit() {
    this.roles = Roles;
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.testForAuthorize();
  }
  testForAuthorize() {
    this.holidayService.showAll();
  }
}
