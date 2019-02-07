import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserData } from '../../InterfacesAndClasses/UserData'
import { AuthorizeService } from '../../Services/authorize.service'
import { Observable, Subscription } from 'rxjs';
import { UserRole, Role, User } from '../../InterfacesAndClasses/UserRole';
import { Router, NavigationExtras, ActivatedRoute } from "@angular/router";
import { Credentials } from '../../InterfacesAndClasses/Credentials'

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.css']
})
export class AuthorizeComponent implements OnInit, OnDestroy { 
  private subscription: Subscription;
  brandNew: boolean;
  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;
  credentials: Credentials = { email: '', password: '' };
  constructor(private authorizeService: AuthorizeService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    // subscribe to router event
    this.subscription = this.activatedRoute.queryParams.subscribe(
      (param: any) => { 
        this.brandNew = param['brandNew'];
        this.credentials.email = param['email'];   
      });
    if (localStorage.getItem('auth_token') != null) {
      this.router.navigate(['/home']);
    };
  }
  ngOnDestroy() {
    // prevent memory leak by unsubscribing
    this.subscription.unsubscribe();
  }

  login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (valid) {
      this.authorizeService.login(value.email, value.password)
        .subscribe(
          result => {
            if (result) {
              window.location.reload();
              this.router.navigate(['/home']);
            }
          },
        error => this.errors = error.error.login_failure);
    }
  }

}
