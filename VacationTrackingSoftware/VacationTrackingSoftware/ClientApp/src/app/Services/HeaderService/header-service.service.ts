import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  currentOperation=1;
  constructor() { }
  getCurrentOperation(item: number): void {
    this.currentOperation = item;
  }
}
