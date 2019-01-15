import { Component, OnInit } from '@angular/core';
import { UserData } from '../../InterfacesAndClasses/UserData'
import { AuthorizeService } from '../../Services/authorize.service'
import { Observable } from 'rxjs';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { Router, NavigationExtras } from "@angular/router";
import { HomeService } from '../../Services/home.service';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.css']
})
export class AuthorizeComponent implements OnInit {
  user: UserData;
  userRole: UserRole;
  constructor(private authorizeService: AuthorizeService, private router: Router, private homeService: HomeService) { }

  ngOnInit() {
  }



  Replace(userName: string, userPassword: string): void{
    //it is optional
    this.user = {
      name: userName,
      password: userPassword
    }
    this.authorizeService.chekUser(this.user).subscribe(userRole => {
      if (userRole.role.id == 1) this.homeService.logInHrUser(userRole.user.id);
      if (userRole.role.id == 2) this.homeService.logInEmployee(userRole.user.id);
      if (userRole.role.id == 3) this.homeService.logInHrUser(userRole.user.id);
      if (userRole != null) this.router.navigate(["home"]);
    });
    
  }
}
