import { Component, OnInit } from '@angular/core';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  public userRole: UserRole;
  public constructor(private route: ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
      this.userRole = JSON.parse(params["userRole"]);
    });
  }

  ngOnInit() {
  }

}
