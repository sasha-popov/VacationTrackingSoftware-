import { Injectable } from '@angular/core';
import { UserData } from '../InterfacesAndClasses/UserData';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpRequestService } from '.././Services/httpRequest.service';
import { UserRole, Role, User } from '.././InterfacesAndClasses/UserRole';
import { Router, NavigationExtras } from "@angular/router";
import { ConfigService } from './ConfigService';
import { BaseService } from '../Services/BaseService';
import { Credentials } from '../InterfacesAndClasses/Credentials';
import { map } from "rxjs/operators";
import { Response } from '_debugger';
import { Response } from 'selenium-webdriver/http';
@Injectable()
export class AuthorizeService extends BaseService {

  baseUrl: string = '';

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;
  credentials: Credentials;
  constructor(private http: HttpRequestService, private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = configService.getApiURI();
  }

  login(userName, password){
    //let headers = new Headers();
    //headers.append('Content-Type', 'application/json');
    this.credentials.email = userName;
    this.credentials.password = password;
    return this.http
      .post(
      this.baseUrl + '/auth/login',
      JSON.stringify({ userName, password })
      ).pipe(map(res => {
          localStorage.setItem('auth_token', res.auth_token);
          this.loggedIn = true;
          this._authNavStatusSource.next(true);
          return true;
        })
      );
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  facebookLogin(accessToken: string) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let body = JSON.stringify({ accessToken });
    return this.http
      .post(
        this.baseUrl + '/externalauth/facebook', body)
      .map(res => res.json())
      .map(res => {
        localStorage.setItem('auth_token', res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }
}
