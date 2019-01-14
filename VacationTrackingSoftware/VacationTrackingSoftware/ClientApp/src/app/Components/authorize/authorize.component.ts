import { Component, OnInit } from '@angular/core';
import { UserData } from '../../InterfacesAndClasses/UserData'
import { AuthorizeService } from '../../Services/authorize.service'
import { Observable } from 'rxjs';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { Router, NavigationExtras } from "@angular/router";

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.css']
})
export class AuthorizeComponent implements OnInit {
  user: UserData;
  userRole: UserRole;
  constructor(private authorizeService: AuthorizeService, private router: Router) { }

  ngOnInit() {
    //this.authorizeService.chekUser;
  }



  Replace(userName: string, userPassword: string): void{
    //it is optional
    this.user = {
      name: userName,
      password: userPassword
    }
    this.authorizeService.chekUser(this.user).subscribe(userRole => this.userRole = userRole);
  }
  Console(): void {
    console.log(this.userRole);
        let navigationExtras: NavigationExtras = {
          queryParams: {
            userRole: JSON.stringify(this.userRole)
          }
    };
    this.router.navigate(["home"], navigationExtras);
  }
}
