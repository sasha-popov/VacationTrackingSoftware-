import { Component, OnInit, Input } from '@angular/core';
import { AuthorizeService } from '../../Services/authorize.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent implements OnInit {
  @Input() title: string;
  userName: string;
  constructor(private authorizeService: AuthorizeService, private router: Router) { }
  ngOnInit() {
    this.userName = localStorage.getItem('name'); 
  }

  logOut() {
    this.authorizeService.logout();
    this.router.navigate(['/']);
  }

}
