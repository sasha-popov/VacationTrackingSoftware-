import { Component, OnInit, Inject, Input, OnChanges } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { UpdateUserService } from '../../Services/update-user.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {
  user: any;
  errors: string;
  success: string;
  constructor(private route: ActivatedRoute, private dialogRef: MatDialogRef<UpdateUserComponent>, private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: any, private updateUserService: UpdateUserService) {
    this.getUser();
  }

  ngOnInit() {

  }
  getUser() {
    this.updateUserService.getCurrentAppUser().subscribe(result => {
      this.user = result;
    })
  }

  updateUser(userName: string, firstName: string, lastName: string, phoneNumber: string, email: string) {
    this.user.userName = userName;
    this.user.firstName = firstName;
    this.user.lastName = lastName;
    this.user.phoneNumber = phoneNumber;
    this.user.email = email;
    this.updateUserService.update(this.user).subscribe(result => {
      if (result.successful == true) {
        this.success = "Congratulation!";
        this.errors = "";
        this.router.navigate(['/']);
      }
      else {
        this.errors = result.errors[0];
        this.success = "";
      }
    }, error => {
      if (error.status != 400) {
        this.success = error.error.text; this.errors = ""; this.router.navigate(['/']);
      }
      else { this.errors = error.error.vacationPolicyError; this.success = "" }
    });
  }
  close() {
    this.dialogRef.close();
  }

}
