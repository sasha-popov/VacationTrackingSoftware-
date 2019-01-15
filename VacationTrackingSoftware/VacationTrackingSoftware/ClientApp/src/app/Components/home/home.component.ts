import { Component, OnInit } from '@angular/core';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { ActivatedRoute } from "@angular/router";
import { HomeService } from '../../Services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  isEmployee: boolean;
  isHrUser: boolean;
  isManager: boolean;
  public constructor(private route: ActivatedRoute, private homeService:HomeService) {
  }

  ngOnInit() {
    this.isEmployee = this.homeService.isEmployee;
    this.isHrUser = this.homeService.isHrUser;
    this.isManager = this.homeService.isManager;
  }

}
