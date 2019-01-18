// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthorizeService } from './authorize.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private user: AuthorizeService, private router: Router) { }

  canActivate() {

    if (!this.user.isLoggedIn()) { 
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
