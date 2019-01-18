import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class HomeService {
   isManager: boolean;
   isHrUser:boolean; 
   isEmployee:boolean;
  //in future need to change
  idUser: number;
  constructor() { 
  }

  logInManager(id: number): void {
    this.isManager = true; 
    this.idUser = id;
  
  }

  logInHrUser(id: number) {
    this.isHrUser = true;
    this.idUser = id;
  }

  logInEmployee(id: number) {
    this.isEmployee = true;
    this.idUser = id;
  }
}
