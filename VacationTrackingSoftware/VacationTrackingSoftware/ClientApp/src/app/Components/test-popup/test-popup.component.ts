import { MatDialog, MatDialogRef } from '@angular/material';
import { CreateEmployeeComponent } from '../create-employee/create-employee.component';
import { Component } from '@angular/core';


@Component({
  selector: 'app-test-popup',
  templateUrl: './test-popup.component.html',
  styleUrls: ['./test-popup.component.css']
})
export class TestPopupComponent {

  fileNameDialogRef: MatDialogRef<CreateEmployeeComponent>;

  constructor(private dialog: MatDialog) { }

  openAddFileDialog() {
    this.fileNameDialogRef = this.dialog.open(CreateEmployeeComponent);
  }
}
