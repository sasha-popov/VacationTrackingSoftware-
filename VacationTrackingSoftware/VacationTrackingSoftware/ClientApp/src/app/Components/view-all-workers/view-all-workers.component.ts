import { Component, OnChanges, Input, OnInit, SimpleChange } from '@angular/core';
import { Roles } from '../../Enums/Roles';
import {allWorkerService } from "../../Services/allWorkerService"
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router, NavigationEnd } from '@angular/router';
import { UpdateWorkerComponent } from '../update-worker/update-worker.component';

@Component({
  selector: 'app-view-all-workers',
  templateUrl: './view-all-workers.component.html',
  styleUrls: ['./view-all-workers.component.css']
})
export class ViewAllWorkersComponent implements OnInit {
  date: string;
  currentRole: any;
  roles;
  errors: string;
  success: string;
  dateNow: Date = new Date();
  dateNowISO = this.dateNow.toISOString();
  workers:{ };
  constructor(private allWorker: allWorkerService, private dialog: MatDialog, private router: Router) {
    this.router.events.subscribe((e: any) => {
      // If it is a NavigationEnd event re-initalise the component
      if (e instanceof NavigationEnd) {
        this.initialiseInvites();
      }
    });
  }
  initialiseInvites() {
    this.showAll();
  }

  ngOnInit() {
    this.currentRole = parseInt(localStorage.getItem('rolesUser'), 10);
    this.roles = Roles;
    this.dateNowISO;
    this.showAll();
  }

  showAll() {
    this.allWorker.showAll().subscribe(workers => {
      this.workers = workers;
    })
  }

  update(worker:any) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    dialogConfig.data = worker;
    let dialogRef = this.dialog.open(UpdateWorkerComponent, dialogConfig);
  }

}
