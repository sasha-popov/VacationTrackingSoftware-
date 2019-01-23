import { Component, OnInit } from '@angular/core';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { ActivatedRoute, Router } from "@angular/router";
import { HomeService } from '../../Services/home.service';
import { AuthorizeService } from '../../Services/authorize.service';
import { Roles } from '../../Roles';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  public constructor(private route: ActivatedRoute, private homeService: HomeService, private router: Router, private authorizeService: AuthorizeService) {
  }
  currentRole: any;
  allRoles;
  ngOnInit() {
    this.allRoles = Roles;
    Roles.Admin;
    this.currentRole = parseInt(localStorage.getItem('rolesUser'),10);
  }
  logOut() {
    this.authorizeService.logout();   
    this.router.navigate(['/']);
  }

}
