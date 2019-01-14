import { Component, OnInit } from '@angular/core';
import { UserData } from '../../InterfacesAndClasses/UserData'
import { AuthorizeService } from '../../Services/authorize.service'
//import { Router } from '@angular.router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.css']
})
export class AuthorizeComponent implements OnInit {
  user: UserData;
  constructor(private authorizeService: AuthorizeService) { }

  ngOnInit() {
    this.authorizeService.chekUser;
  }

  Replace(userName: string, userPassword: string): void{
    this.user = {
      name: userName,
      password: userPassword
    }
    this.authorizeService.chekUser(this.user);
  }

}
